using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CH11Lab.Data;
using CH11Lab.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CH11Lab.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SalesController(ApplicationDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var employees = await _db.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToListAsync();
            ViewBag.Employees = employees;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Sale sale)
        {
            var employees = await _db.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToListAsync();
            ViewBag.Employees = employees;

            if (ModelState.IsValid)
            {
                _db.Sales.Add(sale);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(sale);
        }
    }
}