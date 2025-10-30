using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CH11Lab.Data;
using CH11Lab.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CH11Lab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index(int? employeeId)
        {
            var employees = await _db.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToListAsync();
            ViewBag.Employees = employees;

            var salesQuery = _db.Sales.Include(s => s.Employee).AsQueryable();
            if (employeeId.HasValue && employeeId.Value > 0)
            {
                salesQuery = salesQuery.Where(s => s.EmployeeId == employeeId.Value);
                ViewBag.SelectedEmployeeId = employeeId.Value;
            }

            var sales = await salesQuery
                .OrderByDescending(s => s.Year)
                .ThenBy(s => s.Quarter)
                .ToListAsync();

            ViewBag.TotalSales = sales.Sum(s => s.Amount);

            return View(sales);
        }
    }
}
