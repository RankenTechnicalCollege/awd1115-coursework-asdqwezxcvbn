using Microsoft.AspNetCore.Mvc;

namespace S3FinalV2.Areas.Mechanic.Controllers
{
    public class MechanicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
