using System.Diagnostics;
using CH07Lab.Models;
using Microsoft.AspNetCore.Mvc;

namespace CH07Lab.Controllers
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
            ViewData["Active"] = "Home";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Active"] = "About";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Active"] = "Contact";
            var contacts = new Dictionary<string, string>
            {
                { "Phone", "555-123-4567" },
                { "Email", "me@mywebsite.com" },
                { "Facebook", "facebook.com/mywebsite" }
            };
            return View(contacts);
        }
    }
}
