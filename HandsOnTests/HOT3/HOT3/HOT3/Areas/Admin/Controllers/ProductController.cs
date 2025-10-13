using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOT3.Data;
using HOT3.Models;

namespace HOT3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ShopDbContext _db;
        public ProductController(ShopDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            return View(await _db.Products.OrderBy(p => p.Name).ToListAsync());
        }

        public IActionResult Create() => View(new Product());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (!ModelState.IsValid) return View(model);

            if (string.IsNullOrWhiteSpace(model.Slug))
                model.Slug = Slugify(model.Name);

            if (await _db.Products.AnyAsync(p => p.Slug == model.Slug))
            {
                ModelState.AddModelError(nameof(model.Slug), "Slug must be unique.");
                return View(model);
            }

            _db.Products.Add(model);
            await _db.SaveChangesAsync();
            TempData["Message"] = $"Product '{model.Name}' created.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/admin/product/edit/{slug}/")]
        public async Task<IActionResult> Edit(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) return NotFound();
            var prod = await _db.Products.FirstOrDefaultAsync(p => p.Slug == slug);
            if (prod == null) return NotFound();
            return View(prod);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = await _db.Products.FindAsync(model.ProductId);
            if (existing == null) return NotFound();

            if (await _db.Products.AnyAsync(p => p.Slug == model.Slug && p.ProductId != model.ProductId))
            {
                ModelState.AddModelError(nameof(model.Slug), "Slug must be unique.");
                return View(model);
            }

            existing.Name = model.Name;
            existing.Slug = model.Slug;
            existing.ImageFileName = model.ImageFileName;
            existing.Category = model.Category;
            existing.Manufacturer = model.Manufacturer;
            existing.Stock = model.Stock;
            existing.Price = model.Price;
            existing.Description = model.Description;

            await _db.SaveChangesAsync();
            TempData["Message"] = $"Product '{existing.Name}' updated.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("/admin/product/delete/{slug}/")]
        public async Task<IActionResult> Delete(string slug)
        {
            var prod = await _db.Products.FirstOrDefaultAsync(p => p.Slug == slug);
            if (prod == null) return NotFound();
            return View(prod);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            var prod = await _db.Products.FindAsync(productId);
            if (prod != null)
            {
                _db.Products.Remove(prod);
                await _db.SaveChangesAsync();
                TempData["Message"] = $"Product '{prod.Name}' deleted.";
            }
            return RedirectToAction(nameof(Index));
        }

        private string Slugify(string input)
        {
            var slug = input?.ToLowerInvariant() ?? "";
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\-]", "");
            return slug;
        }
    }
}