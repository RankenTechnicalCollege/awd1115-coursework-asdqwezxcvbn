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

                // Step 2: Load mechanics with current assignments
                var mechanics = await _context.Mechanics
                    .Include(m => m.MechanicAssignments)
                    .ToListAsync();

                Mechanics? chosenMechanic = null;

                foreach (var mech in mechanics)
                {
                    float currentAssignedHours = mech.MechanicAssignments.Sum(ma => ma.AssignedHours);
                    float availableHours = mech.HourlyLimitPerWeek - (mech.TotalHoursWorked + currentAssignedHours);

                    if (mech.SkillLevel >= jobTemplate.SkillLevel && availableHours >= estHours)
                    {
                        if (chosenMechanic == null ||
                            (mech.TotalHoursWorked + currentAssignedHours) <
                            (chosenMechanic.TotalHoursWorked + chosenMechanic.MechanicAssignments.Sum(ma => ma.AssignedHours)))
                        {
                            chosenMechanic = mech;
                        }
                    }
                }

                // Step 3: Assign mechanic if available
                if (chosenMechanic != null)
                {
                    var mechAssign = new MechanicAssignment
                    {
                        MechanicId = chosenMechanic.MechanicId,
                        AssignedJobId = model.AssignedJobId,
                        AssignedHours = estHours,
                        ActualHours = 0f
                    };

                    _context.MechanicAssignments.Add(mechAssign);

                    chosenMechanic.TotalHoursWorked += estHours;
                    _context.Mechanics.Update(chosenMechanic);

                    await _context.SaveChangesAsync();

                    Console.WriteLine($"Job {model.AssignedJobId} assigned to mechanic {chosenMechanic.Name}");
                }
                else
                {
                    TempData["AssignmentNotice"] = "No mechanic available for assignment.";
                    Console.WriteLine($"Job {model.AssignedJobId} could not be assigned to any mechanic.");
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return RedirectToAction(nameof(Index));
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

            _context.MechanicAssignments.RemoveRange(assigned.MechanicAssignments);
            _context.AssignedJobs.Remove(assigned);
            await _context.SaveChangesAsync();

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

        [HttpPost]
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

            var mechAssign = assigned.MechanicAssignments?.FirstOrDefault();
            if (mechAssign != null)
            {
                mechAssign.ActualHours = actualHours;
                _context.MechanicAssignments.Update(mechAssign);

                var mech = await _context.Mechanics.FindAsync(mechAssign.MechanicId);
                if (mech != null)
                {
                    mech.TotalHoursWorked = mech.TotalHoursWorked - mechAssign.AssignedHours + actualHours;
                    if (mech.TotalHoursWorked < 0) mech.TotalHoursWorked = 0;
                    _context.Mechanics.Update(mech);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> UpdatePriority(int id)
        {
            var assigned = await _context.AssignedJobs.FindAsync(id);
            if (assigned == null) return NotFound();

            ViewData["Priorities"] = new SelectList(new[] { "High", "Medium", "Low" }, assigned.Priority);
            return View(assigned);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePriorityConfirmed(int id, string priority)
        {
            var assigned = await _context.AssignedJobs.FindAsync(id);
            if (assigned == null) return NotFound();

            assigned.Priority = priority;
            _context.AssignedJobs.Update(assigned);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}