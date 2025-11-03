using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentScheduler.Data;
using AppointmentScheduler.Models;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _db;
        public AppointmentsController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var appts = await _db.Appointments.Include(a => a.Customer).OrderBy(a => a.StartDateTime).ToListAsync();
            return View(appts);
        }

        public async Task<IActionResult> Create()
        {
            var customers = await _db.Customers.OrderBy(c => c.Username).ToListAsync();
            ViewData["CustomerId"] = new SelectList(customers, "Id", "Username");

            var nextHour = DateTime.Now.AddHours(1);
            var defaultStart = new DateTime(nextHour.Year, nextHour.Month, nextHour.Day, nextHour.Hour, 0, 0);
            var model = new Appointment { StartDateTime = defaultStart };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                var customers = await _db.Customers.OrderBy(c => c.Username).ToListAsync();
                ViewData["CustomerId"] = new SelectList(customers, "Id", "Username", appointment.CustomerId);
                return View(appointment);
            }

            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}