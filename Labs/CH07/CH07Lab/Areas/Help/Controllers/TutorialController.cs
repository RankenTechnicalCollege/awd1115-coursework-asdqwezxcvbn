using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.Areas.Help.Controllers
{
    [Area("Help")]
    public class TutorialController : Controller
    {
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = "Page1";

            switch (id.ToLower())
            {
                case "page1": return View("Page1");
                case "page2": return View("Page2");
                case "page3": return View("Page3");
                default: return View("Page1");
            }
        }
    }
}