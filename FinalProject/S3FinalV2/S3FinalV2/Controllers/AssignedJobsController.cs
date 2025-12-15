using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using S3FinalV2.Data;
using S3FinalV2.Models;

namespace S3FinalV2.Controllers
{
    public class AssignedJobsController : Controller
    {
        private readonly MechTrackDbContext _context;

        public AssignedJobsController(MechTrackDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var jobs = await _context.AssignedJobs
                .Include(a => a.Jobs)
                .Include(a => a.Customer)
                .Include(a => a.MechanicAssignments)
                    .ThenInclude(ma => ma.Mechanic)
                .ToListAsync();

            return View(jobs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a.Jobs)
                .Include(a => a.Customer)
                .Include(a => a.MechanicAssignments)
                    .ThenInclude(ma => ma.Mechanic)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null) return NotFound();

            return View(assigned);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Customers"] = new SelectList(await _context.Customers.ToListAsync(), "CustomerId", "Name");
            ViewData["Jobs"] = new SelectList(await _context.Jobs.ToListAsync(), "JobId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignedJobs model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Customers"] = new SelectList(await _context.Customers.ToListAsync(), "CustomerId", "Name", model.CustomerId);
                ViewData["Jobs"] = new SelectList(await _context.Jobs.ToListAsync(), "JobId", "Name", model.JobsId);
                return View(model);
            }

            var jobTemplate = await _context.Jobs.FindAsync(model.JobsId);
            if (jobTemplate == null) return BadRequest("Job template not found.");

            // Parse estimated hours
            float estHours = 0f;
            if (!string.IsNullOrWhiteSpace(jobTemplate.EstCompTime))
            {
                float.TryParse(jobTemplate.EstCompTime, NumberStyles.Float, CultureInfo.InvariantCulture, out estHours);
            }

            model.EstimatedHours = estHours;
            model.CreatedDate = DateTime.UtcNow;

            // Transaction ensures atomic operation
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Save job first to generate AssignedJobId
                _context.AssignedJobs.Add(model);
                await _context.SaveChangesAsync();

                // Step 2: Attempt automatic mechanic assignment
                await AssignMechanicToJobAsync(model, jobTemplate, estHours);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error creating assigned job: {ex.Message}");
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task AssignMechanicToJobAsync(AssignedJobs assignedJob, Jobs jobTemplate, float estHours)
        {
            var mechanics = await _context.Mechanics
                .Include(m => m.MechanicAssignments)
                .ToListAsync();

            Console.WriteLine($"[DEBUG] Total mechanics found: {mechanics.Count}");
            foreach (var m in mechanics)
            {
                Console.WriteLine($"[DEBUG] Mechanic: {m.Name}, SkillLevel: {m.SkillLevel}, HourlyLimitPerWeek: {m.HourlyLimitPerWeek}, TotalHoursWorked: {m.TotalHoursWorked}");
            }

            var chosenMechanic = mechanics
                .Where(m =>
                {
                    bool skillMatch = m.SkillLevel >= jobTemplate.SkillLevel;
                    float availableHours = m.HourlyLimitPerWeek - m.TotalHoursWorked;
                    bool hoursMatch = availableHours >= estHours;
                    Console.WriteLine($"[DEBUG] Evaluating {m.Name}: SkillMatch={skillMatch}, AvailableHours={availableHours}, HoursMatch={hoursMatch}");
                    return skillMatch && hoursMatch;
                })
                .OrderBy(m => m.TotalHoursWorked)
                .ThenBy(m => m.MechanicId)
                .FirstOrDefault();

            if (chosenMechanic != null)
            {
                var mechAssign = new MechanicAssignment
                {
                    MechanicId = chosenMechanic.MechanicId,
                    AssignedJobId = assignedJob.AssignedJobId,
                    AssignedHours = estHours,
                    ActualHours = 0f
                };

                _context.MechanicAssignments.Add(mechAssign);

                var mechToUpdate = await _context.Mechanics.FindAsync(chosenMechanic.MechanicId);
                if (mechToUpdate != null)
                {
                    mechToUpdate.TotalHoursWorked += estHours;
                    _context.Mechanics.Update(mechToUpdate);
                }

                await _context.SaveChangesAsync();
                Console.WriteLine($"Job {assignedJob.AssignedJobId} assigned to mechanic {chosenMechanic.Name}");
            }
            else
            {
                TempData["AssignmentNotice"] = "No mechanic available for assignment.";
                Console.WriteLine($"Job {assignedJob.AssignedJobId} could not be assigned. Required skill: {jobTemplate.SkillLevel}, Required hours: {estHours}");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var assigned = await _context.AssignedJobs.FindAsync(id);
            if (assigned == null) return NotFound();
            return View(assigned);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a.MechanicAssignments)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null) return NotFound();

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var mechAssign in assigned.MechanicAssignments)
                {
                    var mech = await _context.Mechanics.FindAsync(mechAssign.MechanicId);
                    if (mech != null)
                    {
                        mech.TotalHoursWorked -= mechAssign.AssignedHours;
                        if (mech.TotalHoursWorked < 0) mech.TotalHoursWorked = 0;
                        _context.Mechanics.Update(mech);
                    }
                }

                _context.MechanicAssignments.RemoveRange(assigned.MechanicAssignments);
                _context.AssignedJobs.Remove(assigned);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Complete(int id)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a.Jobs)
                .Include(a => a.Customer)
                .Include(a => a.MechanicAssignments)
                    .ThenInclude(ma => ma.Mechanic)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null) return NotFound();

            ViewData["AssignedHours"] = assigned.MechanicAssignments?.FirstOrDefault()?.AssignedHours ?? 0f;
            return View(assigned);
        }

        // Map POSTs that post to action "Complete" (common in Razor forms) to this handler.
        [HttpPost, ActionName("Complete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteConfirmed(int id, float actualHours)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a.MechanicAssignments)
                    .ThenInclude(ma => ma.Mechanic)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null) return NotFound();

            assigned.IsCompleted = true;
            assigned.ActualHours = actualHours;
            _context.AssignedJobs.Update(assigned);

            var mechAssigns = assigned.MechanicAssignments?.ToList() ?? new List<MechanicAssignment>();
            float totalAssigned = mechAssigns.Sum(ma => ma.AssignedHours);

            foreach (var mechAssign in mechAssigns)
            {
                // Distribute actual hours proportionally based on assigned hours
                float proportion = totalAssigned > 0f ? (mechAssign.AssignedHours / totalAssigned) : 0f;
                float mechActual = actualHours * proportion;

                mechAssign.ActualHours = mechActual;
                _context.MechanicAssignments.Update(mechAssign);

                var mech = await _context.Mechanics.FindAsync(mechAssign.MechanicId);
                if (mech != null)
                {
                    mech.TotalHoursWorked = mech.TotalHoursWorked - mechAssign.AssignedHours + mechActual;
                    if (mech.TotalHoursWorked < 0) mech.TotalHoursWorked = 0;
                    _context.Mechanics.Update(mech);
                }
            }

            await _context.SaveChangesAsync();

            // Return to Index as requested
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdatePriority(int id)
        {
            var assigned = await _context.AssignedJobs.FindAsync(id);
            if (assigned == null) return NotFound();

            ViewData["Priorities"] = new SelectList(new[] { "High", "Medium", "Low" }, assigned.Priority);
            return View(assigned);
        }

        // Map POSTs that post to action "UpdatePriority" (common in Razor forms) to this handler.
        [HttpPost, ActionName("UpdatePriority")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePriorityConfirmed(int id, string priority)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a.Jobs)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null) return NotFound();

            assigned.Priority = priority;
            _context.AssignedJobs.Update(assigned);

            // Also update the job template priority to keep in sync (if present)
            if (assigned.Jobs != null)
            {
                assigned.Jobs.Priority = priority;
                _context.Jobs.Update(assigned.Jobs);
            }

            await _context.SaveChangesAsync();

            // Return to Index as requested
            return RedirectToAction(nameof(Index));
        }
    }
}