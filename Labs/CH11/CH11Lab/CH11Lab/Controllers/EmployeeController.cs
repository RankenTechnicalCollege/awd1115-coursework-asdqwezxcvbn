using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CH11Lab.Data;
using CH11Lab.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CH11Lab.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var managers = await _db.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToListAsync();
            ViewBag.Managers = managers;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Employee employee)
        {
            var managers = await _db.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToListAsync();
            ViewBag.Managers = managers;

            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(employee);
        }
    }
}