using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentScheduler.Data;
using AppointmentScheduler.Models;

namespace AppointmentScheduler.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _db;
        public CustomersController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.Customers.OrderBy(c => c.Username).ToListAsync();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);

            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}