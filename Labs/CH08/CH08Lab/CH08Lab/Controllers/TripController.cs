using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TripLog.Data;
using TripLog.Helpers;
using TripLog.Models;
using TripLog.ViewModels;

namespace TripLog.Controllers
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

        // Home page - list trips
        public async Task<IActionResult> Index()
        {
            var trips = await _context.Trips.OrderByDescending(t => t.TripId).ToListAsync();
            return View(trips);
        }

        #region Add Flow - Page 1
        [HttpGet]
        public IActionResult AddPage1()
        {
            TempData.Remove(TempKeyPage1);
            TempData.Remove(TempKeyPage2);
            var vm = TempData.Get<TripPage1ViewModel>(TempKeyPage1) ?? new TripPage1ViewModel();
            ViewBag.SubHeader = string.Empty;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPage1(TripPage1ViewModel model, string action)
        {
            if (action == "Cancel")
            {
                TempData.Clear();
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                ViewBag.SubHeader = string.Empty;
                return View(model);
            }

            TempData.Put(TempKeyPage1, model);
            ViewBag.SubHeader = model.Accommodation;
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

            ViewBag.SubHeader = page1.Accommodation;
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
        public IActionResult AddPage3()
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

            ViewBag.SubHeader = page1.Destination;
            var vm = new TripPage3ViewModel();
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

            var trip = new Trip
            {
                Destination = page1.Destination,
                Accommodation = page1.Accommodation,
                StartDate = page1.StartDate!.Value,
                EndDate = page1.EndDate!.Value,
                AccommodationPhone = page2?.AccommodationPhone,
                AccommodationEmail = page2?.AccommodationEmail,
                Activity1 = model.Activity1,
                Activity2 = model.Activity2,
                Activity3 = model.Activity3
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            TempData.Clear();
            TempData["TempMessage"] = $"Trip to {trip.Destination} added.";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        [HttpPost]
        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}