using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOT3.Data;

namespace HOT3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopDbContext _db;
        public HomeController(ShopDbContext db) { _db = db; }

        public async Task<IActionResult> Index(string? category)
        {
            var q = _db.Products.AsQueryable();
            ViewBag.Categories = await _db.Products.Select(p => p.Category).Distinct().ToListAsync();
            if (!string.IsNullOrWhiteSpace(category))
            {
                q = q.Where(p => p.Category.ToLower() == category.ToLower());
                ViewBag.Filter = category;
            }

            var list = await q.OrderBy(p => p.Name).ToListAsync();
            return View(list);
        }

        [HttpGet("/product/details/{slug}/")]
        public async Task<IActionResult> Details(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return NotFound();
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Slug == slug);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}