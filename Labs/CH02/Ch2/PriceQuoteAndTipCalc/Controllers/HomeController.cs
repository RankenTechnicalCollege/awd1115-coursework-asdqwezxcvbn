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

        [HttpGet]
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

        [HttpGet]
        public IActionResult TipCalculator()
        {
            return View(new TipCalculator());
        }

        [HttpPost]
        public IActionResult TipCalculator(TipCalculator model)
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
