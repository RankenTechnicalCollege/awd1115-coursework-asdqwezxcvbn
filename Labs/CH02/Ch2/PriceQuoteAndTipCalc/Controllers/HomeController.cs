using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PriceQuoteAndTipCalc.Models;

namespace PriceQuoteAndTipCalc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult PriceQuote(PriceQuote model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View(model);
        }

        public IActionResult TipCalculator(TipCalculator model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View(model);
        }

    }
}
