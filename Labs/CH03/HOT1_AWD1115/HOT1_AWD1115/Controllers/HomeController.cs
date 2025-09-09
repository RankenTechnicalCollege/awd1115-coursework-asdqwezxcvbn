using HOT1_AWD1115.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HOT1_AWD1115.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult DistanceConverter()
        {
            return View(new DistanceConverter());
        }

        [HttpPost]
        public IActionResult DistanceConverter(DistanceConverter model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Centimeters = model.Centimeters;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult OrderForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderForm(OrderForm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.DiscountCode) && !model.IsDiscountValid)
            {
                ViewBag.ErrorMessage = "Invalid discount code. No discount applied.";
            }

            return View(model);
        }
    }
}
