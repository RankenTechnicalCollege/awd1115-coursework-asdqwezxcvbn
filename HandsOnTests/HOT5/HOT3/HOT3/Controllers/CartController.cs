using HOT3.Data;
using HOT3.Helpers;
using HOT3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HOT3.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext _db;
        private const string SessionKey = "cart";
        public CartController(ShopDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var items = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            var vm = new CartViewModel { Items = items };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            var existing = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existing != null) existing.Quantity += quantity;
            else cart.Add(new CartItem
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Slug = product.Slug,
                ImageFileName = product.ImageFileName,
                UnitPrice = product.Price,
                Quantity = quantity
            });

            HttpContext.Session.SetObjectAsJson(SessionKey, cart);
            TempData["CartMessage"] = $"{product.Name} added to cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveItem(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObjectAsJson(SessionKey, cart);
                TempData["CartMessage"] = $"{item.Name} removed from cart.";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            if (!cart.Any())
            {
                TempData["CartMessage"] = "Cart is empty.";
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                CreatedAt = DateTime.UtcNow,
                TotalQuantity = cart.Sum(i => i.Quantity),
                TotalPrice = cart.Sum(i => i.LineTotal),
                Items = cart.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Name,
                    ProductSlug = ci.Slug,
                    UnitPrice = ci.UnitPrice,
                    Quantity = ci.Quantity
                }).ToList()
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            HttpContext.Session.Remove(SessionKey);
            TempData["CartMessage"] = "Thank you — purchase completed!";
            return RedirectToAction("Index");
        }
    }
}