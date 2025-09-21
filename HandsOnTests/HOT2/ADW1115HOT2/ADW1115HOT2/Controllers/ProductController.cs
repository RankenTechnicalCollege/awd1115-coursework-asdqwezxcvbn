using ADW1115HOT2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ADW1115HOT2.Controllers
{
    public class ProductController : Controller
    {
        private readonly SalesOrdersContext _context;

        public ProductController(SalesOrdersContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.ProductName)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(),
                "CategoryID",
                "CategoryName");

            if (id == null)
                return View(new Product());

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(),
                "CategoryID",
                "CategoryName",
                product.CategoryID);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(),
                    "CategoryID",
                    "CategoryName",
                    product.CategoryID);
                return View(product);
            }

            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                _context.Products.Update(product);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            return product == null ? NotFound() : View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("List");
        }
    }
}