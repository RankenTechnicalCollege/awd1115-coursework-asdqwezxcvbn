using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using S3FinalV2.Models;

namespace S3FinalV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int visits = HttpContext.Session.GetInt32("VisitCount") ?? 0;
            visits++;
            HttpContext.Session.SetInt32("VisitCount", visits);
            ViewBag.VisitCount = visits;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
