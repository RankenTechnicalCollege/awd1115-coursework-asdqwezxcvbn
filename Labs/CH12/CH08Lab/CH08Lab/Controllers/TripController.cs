using CH08Lab.Data;
using CH08Lab.Models;
using CH08Lab.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CH08Lab.Controllers
{
    public class TripController : Controller
    {
        private readonly TripDbContext _context;
        private const string TempKeyPage1 = "TripPage1";
        private const string TempKeyPage2 = "TripPage2";

        public TripController(TripDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trips = await _context.Trips.OrderByDescending(t => t.TripId).ToListAsync();
            return View(trips);
        }

        #region Add Flow - Page 1
        [HttpGet]
        public async Task<IActionResult> AddPage1()
        {
            TempData.Remove(TempKeyPage1);
            TempData.Remove(TempKeyPage2);
            var vm = TempData.Get<TripPage1ViewModel>(TempKeyPage1) ?? new TripPage1ViewModel();
            vm.Destinations = await _context.Destinations.OrderBy(d => d.Name).ToListAsync();
            vm.Accommodations = await _context.Accommodations.OrderBy(a => a.Name).ToListAsync();
            ViewBag.SubHeader = string.Empty;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPage1(TripPage1ViewModel model, string action)
        {
            if (action == "Cancel")
            {
                TempData.Clear();
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                model.Destinations = await _context.Destinations.OrderBy(d => d.Name).ToListAsync();
                model.Accommodations = await _context.Accommodations.OrderBy(a => a.Name).ToListAsync();
                return View(model);
            }

            TempData.Put(TempKeyPage1, model);
            return RedirectToAction(nameof(AddPage2));
        }
        #endregion

        #region Add Flow - Page 2
        [HttpGet]
        public IActionResult AddPage2()
        {
            var page1 = TempData.Get<TripPage1ViewModel>(TempKeyPage1);
            if (page1 == null)
            {
                TempData.Clear();
                return RedirectToAction(nameof(AddPage1));
            }

            TempData.Put(TempKeyPage1, page1);

            var vm = TempData.Get<TripPage2ViewModel>(TempKeyPage2) ?? new TripPage2ViewModel();

            ViewBag.SubHeader = page1.AccommodationId;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPage2(TripPage2ViewModel model, string action)
        {
            if (action == "Cancel")
            {
                TempData.Clear();
                return RedirectToAction(nameof(Index));
            }

            TempData.Put(TempKeyPage2, model);

            var page1 = TempData.Get<TripPage1ViewModel>(TempKeyPage1);
            if (page1 == null)
            {
                TempData.Clear();
                return RedirectToAction(nameof(AddPage1));
            }

            TempData.Put(TempKeyPage1, page1);

            return RedirectToAction(nameof(AddPage3));
        }
        #endregion

        #region Add Flow - Page 3
        [HttpGet]
        public async Task<IActionResult> AddPage3()
        {
            var page1 = TempData.Get<TripPage1ViewModel>(TempKeyPage1);
            var page2 = TempData.Get<TripPage2ViewModel>(TempKeyPage2);

            if (page1 == null)
            {
                TempData.Clear();
                return RedirectToAction(nameof(AddPage1));
            }

            TempData.Put(TempKeyPage1, page1);
            if (page2 != null) TempData.Put(TempKeyPage2, page2);

            var vm = new TripPage3ViewModel
            {
                Activities = await _context.Activities.OrderBy(a => a.Name).ToListAsync()
            };

            ViewBag.SubHeader = page1.DestinationId;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPage3(TripPage3ViewModel model, string action)
        {
            if (action == "Cancel")
            {
                TempData.Clear();
                return RedirectToAction(nameof(Index));
            }

            var page1 = TempData.Get<TripPage1ViewModel>(TempKeyPage1);
            var page2 = TempData.Get<TripPage2ViewModel>(TempKeyPage2);

            if (page1 == null)
            {
                TempData.Clear();
                return RedirectToAction(nameof(AddPage1));
            }

            var selectedActivities = await _context.Activities
                .Where(a => model.SelectedActivityIds.Contains(a.ActivityId))
                .ToListAsync();

            var trip = new Trip
            {
                DestinationId = page1.DestinationId,
                AccommodationId = page1.AccommodationId,
                StartDate = page1.StartDate!.Value,
                EndDate = page1.EndDate!.Value,
                AccommodationPhone = page2?.AccommodationPhone,
                AccommodationEmail = page2?.AccommodationEmail,
                Activities = selectedActivities
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            var destination = await _context.Destinations
                .FirstOrDefaultAsync(d => d.DestinationId == trip.DestinationId);

            TempData.Clear();
            TempData["TempMessage"] = $"Trip to {destination?.Name} added.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        [HttpPost]
        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction(nameof(Index));
        }

        #region Add Flow - Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var trip = await _context.Trips
                .Include(t => t.Destination)
                .Include(t => t.Accommodation)
                .FirstOrDefaultAsync(t => t.TripId == id);

            if (trip == null) return NotFound();

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int tripId)
        {
            var trip = await _context.Trips
                .Include(t => t.Destination)
                .Include(t => t.Accommodation)
                .FirstOrDefaultAsync(t => t.TripId == tripId);

            if (trip != null)
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
                TempData["TempMessage"] = $"Trip to {trip.Destination?.Name ?? "Unknown"} deleted.";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}