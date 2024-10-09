using Microsoft.AspNetCore.Mvc;

namespace Kawkaba.Controllers.MVC
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
