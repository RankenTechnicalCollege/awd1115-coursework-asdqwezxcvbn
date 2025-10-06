using Microsoft.AspNetCore.Mvc;

namespace CH07Lab.Controllers
{
    public class TutorialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
