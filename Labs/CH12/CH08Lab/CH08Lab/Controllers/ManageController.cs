using CH08Lab.Data;
using CH08Lab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CH08Lab.Controllers
{
    public class ManageController : Controller
    {
        private readonly TripDbContext _context;

        public ManageController(TripDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Destinations()
        {
            var destinations = await _context.Destinations
                .Include(d => d.Trips)
                .ToListAsync();
            return View(destinations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDestination(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Destination name cannot be empty.";
                return RedirectToAction(nameof(Destinations));
            }

            var exists = await _context.Destinations.AnyAsync(d => d.Name.ToLower() == name.Trim().ToLower());
            if (!exists)
            {
                _context.Destinations.Add(new Destination { Name = name.Trim() });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Destinations));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var destination = await _context.Destinations
                .Include(d => d.Trips)
                .FirstOrDefaultAsync(d => d.DestinationId == id);

            if (destination == null) return NotFound();

            if (destination.Trips.Any())
            {
                TempData["Error"] = "Cannot delete a destination linked to trips.";
                return RedirectToAction(nameof(Destinations));
            }

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Destinations));
        }

        public async Task<IActionResult> Accommodations()
        {
            var accommodations = await _context.Accommodations
                .Include(a => a.Trips)
                .ToListAsync();
            return View(accommodations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccommodation(string name, string? phone, string? email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Accommodation name cannot be empty.";
                return RedirectToAction(nameof(Accommodations));
            }

            var exists = await _context.Accommodations.AnyAsync(a => a.Name.ToLower() == name.Trim().ToLower());
            if (!exists)
            {
                _context.Accommodations.Add(new Accommodation
                {
                    Name = name.Trim(),
                    Phone = phone,
                    Email = email
                });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Accommodations));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            var accommodation = await _context.Accommodations
                .Include(a => a.Trips)
                .FirstOrDefaultAsync(a => a.AccommodationId == id);

            if (accommodation == null) return NotFound();

            if (accommodation.Trips.Any())
            {
                TempData["Error"] = "Cannot delete an accommodation linked to trips.";
                return RedirectToAction(nameof(Accommodations));
            }

            _context.Accommodations.Remove(accommodation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Accommodations));
        }

        public async Task<IActionResult> Activities()
        {
            var activities = await _context.Activities.ToListAsync();
            return View(activities);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActivity(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["Error"] = "Activity name cannot be empty.";
                return RedirectToAction(nameof(Activities));
            }

            var exists = await _context.Activities.AnyAsync(a => a.Name.ToLower() == name.Trim().ToLower());
            if (!exists)
            {
                _context.Activities.Add(new Activity { Name = name.Trim() });
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Activities));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null) return NotFound();

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Activities));
        }
    }
}