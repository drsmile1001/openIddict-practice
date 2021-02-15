using Microsoft.AspNetCore.Mvc;

namespace OpenIddictPractice.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["User"] = User.Identity?.Name;
            return View();
        }
    }
}