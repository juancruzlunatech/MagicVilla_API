using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_WebPage.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
