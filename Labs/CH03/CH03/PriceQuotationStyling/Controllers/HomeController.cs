using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PriceQuotationStyling.Models;

namespace PriceQuotationStyling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult PriceQuote()
        {
            return View(new PriceQuote());
        }

        [HttpPost]
        public IActionResult PriceQuote(PriceQuote model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
    }
}
