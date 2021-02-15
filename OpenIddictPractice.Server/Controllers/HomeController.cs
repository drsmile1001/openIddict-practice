using Microsoft.AspNetCore.Mvc;

namespace OpenIddictPractice.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}