using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S3FinalV2.Data;
using S3FinalV2.Models;
using Microsoft.AspNetCore.Identity;

namespace S3FinalV2.Controllers
{
    public class AssignedJobsController : Controller
    {
        private readonly MechTrackDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignedJobsController(MechTrackDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AssignedJobs
        // optional mechanicId to filter
        public async Task<IActionResult> Index(int? mechanicId)
        {
            var q = _context.AssignedJobs
                .Include(a => a.Jobs)
                .Include(a => a.Customer)
                .Include(a => a.Jobs)
                .Include(a => a) // keep includes concise; we use MechanicAssignment separately
                .AsQueryable();

            // Load mechanic assignments and mechanic data when needed in view via separate query
            var list = await q.ToListAsync();

            ViewBag.Mechanics = await _context.Mechanics.ToListAsync();
            ViewBag.SelectedMechanicId = mechanicId;

            if (mechanicId.HasValue)
            {
                list = list.Where(a =>
                    _context.MechanicAssignments.Any(ma => ma.JobId == a.AssignedJobId && ma.MechanicId == mechanicId.Value)
                ).ToList();
            }

            return View(list);
        }

        // GET: AssignedJobs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a.Jobs)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null) return NotFound();
            return View(assigned);
        }

        // GET: AssignedJobs/Create
        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Jobs = _context.Jobs.ToList();
            return View();
        }

        // POST: AssignedJobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignedJobs model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = _context.Customers.ToList();
                ViewBag.Jobs = _context.Jobs.ToList();
                return View(model);
            }

            // load job type (job template)
            var jobType = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == model.JobsId);
            if (jobType == null)
            {
                ModelState.AddModelError("", "Selected job type not found.");
                ViewBag.Customers = _context.Customers.ToList();
                ViewBag.Jobs = _context.Jobs.ToList();
                return View(model);
            }

            var requiredSkill = jobType.SkillLevel;
            var estHours = jobType.EstCompTime > 0 ? jobType.EstCompTime : jobType.AvgCompletionTime;

            // find mechanics with enough skill
            var eligible = await _context.Mechanics
                .Where(m => m.SkillLevel >= requiredSkill)
                .OrderBy(m => m.TotalHours)    // least total hours first
                .ThenBy(m => m.SkillLevel)     // then lower skill (match original logic)
                .ToListAsync();

            if (!eligible.Any())
            {
                ModelState.AddModelError("", "No mechanics with the required skill level are available.");
                ViewBag.Customers = _context.Customers.ToList();
                ViewBag.Jobs = _context.Jobs.ToList();
                return View(model);
            }

            // prefer mechanics who will not exceed weekly limit
            Mechanics selected = eligible.FirstOrDefault(m => (m.TotalHours + estHours) <= m.WeeklyHourLimit);

            if (selected == null)
            {
                // If none fit the weekly limit, mimic your old behavior: indicate cannot assign
                ModelState.AddModelError("", "Cannot assign job: all mechanics would exceed their weekly hour limit.");
                ViewBag.Customers = _context.Customers.ToList();
                ViewBag.Jobs = _context.Jobs.ToList();
                return View(model);
            }

            // populate model fields
            model.CreatedDate = DateTime.Now;
            model.ActualCompTime = null;
            model.IsCompleted = false;
            model.Priority ??= jobType.Priority ?? "Normal";

            // save AssignedJobs record
            _context.AssignedJobs.Add(model);
            await _context.SaveChangesAsync(); // now model.AssignedJobId is populated

            // create MechanicAssignment
            var mechAssign = new MechanicAssignment
            {
                MechanicId = selected.MechanicId,
                JobId = model.AssignedJobId,
                TimeAssigned = estHours,
                TimeCompleted = 0
            };
            _context.MechanicAssignments.Add(mechAssign);

            // update mechanic hours and assigned jobs array (if you want to keep arrays consistent)
            selected.TotalHours += estHours;

            // add to AssignedJobs array (preserve existing)
            var list = selected.AssignedJobs?.ToList() ?? new List<int>();
            list.Add(model.AssignedJobId);
            selected.AssignedJobs = list.ToArray();

            _context.Mechanics.Update(selected);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: AssignedJobs/TakeJob
        // current mechanic (based on logged-in user) takes job from whoever had it
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeJob(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var currentMechanic = await _context.Mechanics.FirstOrDefaultAsync(m => m.UserId == user.Id);
            if (currentMechanic == null) return Forbid();

            var assignment = await _context.MechanicAssignments.FirstOrDefaultAsync(ma => ma.JobId == jobId);
            if (assignment == null) return NotFound();

            // previous mechanic
            var prev = await _context.Mechanics.FirstOrDefaultAsync(m => m.MechanicId == assignment.MechanicId);
            if (prev != null)
            {
                // remove job id from prev.AssignedJobs
                prev.AssignedJobs = prev.AssignedJobs?.Where(x => x != jobId).ToArray() ?? Array.Empty<int>();
                // subtract assigned reserved hours
                prev.TotalHours -= assignment.TimeAssigned;
                if (prev.TotalHours < 0) prev.TotalHours = 0;
                _context.Mechanics.Update(prev);
            }

            // assign to currentMechanic
            assignment.MechanicId = currentMechanic.MechanicId;
            currentMechanic.AssignedJobs = (currentMechanic.AssignedJobs ?? Array.Empty<int>()).Concat(new[] { jobId }).ToArray();
            currentMechanic.TotalHours += assignment.TimeAssigned;

            _context.MechanicAssignments.Update(assignment);
            _context.Mechanics.Update(currentMechanic);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: AssignedJobs/CompleteJob
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteJob(int jobId, float actualHours)
        {
            var assigned = await _context.AssignedJobs.FirstOrDefaultAsync(a => a.AssignedJobId == jobId);
            if (assigned == null) return NotFound();

            var assignment = await _context.MechanicAssignments.FirstOrDefaultAsync(ma => ma.JobId == jobId);
            if (assignment == null) return BadRequest();

            var mech = await _context.Mechanics.FirstOrDefaultAsync(m => m.MechanicId == assignment.MechanicId);
            if (mech == null) return BadRequest();

            // mark complete
            assigned.ActualCompTime = actualHours;
            assigned.IsCompleted = true;

            // update assignment record
            assignment.TimeCompleted = actualHours;

            // adjust mechanic total hours: remove reserved, add actual
            mech.TotalHours -= assignment.TimeAssigned;
            mech.TotalHours += actualHours;
            if (mech.TotalHours < 0) mech.TotalHours = 0;

            // move job id into CompletedJobs array
            mech.AssignedJobs = mech.AssignedJobs?.Where(x => x != jobId).ToArray() ?? Array.Empty<int>();
            var completed = mech.CompletedJobs?.ToList() ?? new List<int>();
            completed.Add(jobId);
            mech.CompletedJobs = completed.ToArray();

            _context.AssignedJobs.Update(assigned);
            _context.MechanicAssignments.Update(assignment);
            _context.Mechanics.Update(mech);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET Delete confirmation
        public async Task<IActionResult> Delete(int id)
        {
            var assigned = await _context.AssignedJobs.FirstOrDefaultAsync(a => a.AssignedJobId == id);
            if (assigned == null) return NotFound();
            return View(assigned);
        }

        // POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null) return NotFound();

            // remove any mechanic assignments and adjust hours
            var assignments = await _context.MechanicAssignments.Where(ma => ma.JobId == id).ToListAsync();
            foreach (var ma in assignments)
            {
                var mech = await _context.Mechanics.FirstOrDefaultAsync(m => m.MechanicId == ma.MechanicId);
                if (mech != null)
                {
                    mech.AssignedJobs = mech.AssignedJobs?.Where(x => x != id).ToArray() ?? Array.Empty<int>();
                    mech.TotalHours -= ma.TimeAssigned;
                    if (mech.TotalHours < 0) mech.TotalHours = 0;
                    _context.Mechanics.Update(mech);
                }

                _context.MechanicAssignments.Remove(ma);
            }

            _context.AssignedJobs.Remove(assigned);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}