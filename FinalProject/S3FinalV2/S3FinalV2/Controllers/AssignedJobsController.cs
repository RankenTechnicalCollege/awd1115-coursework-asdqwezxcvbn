using Microsoft.AspNetCore.Mvc;
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

            if (assigned == null)
                return NotFound();

            return View(assigned);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Jobs = _context.Jobs.ToList();
            return View();
        }

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

            model.CreatedDate = DateTime.Now;

            _context.AssignedJobs.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AssignMechanic(int assignedJobId, int mechanicId)
        {
            var assignment = new MechanicAssignment
            {
                AssignedJobId = assignedJobId,
                MechanicId = mechanicId
            };

            _context.MechanicAssignments.Add(assignment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var assigned = await _context.AssignedJobs.FindAsync(id);
            if (assigned == null)
                return NotFound();

            return View(assigned);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assigned = await _context.AssignedJobs
                .Include(a => a.MechanicAssignments)
                .FirstOrDefaultAsync(a => a.AssignedJobId == id);

            if (assigned == null)
                return NotFound();

            _context.MechanicAssignments.RemoveRange(assigned.MechanicAssignments);
            _context.AssignedJobs.Remove(assigned);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}