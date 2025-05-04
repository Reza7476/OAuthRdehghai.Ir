using Microsoft.AspNetCore.Mvc;

namespace OAuth.Presentation.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
