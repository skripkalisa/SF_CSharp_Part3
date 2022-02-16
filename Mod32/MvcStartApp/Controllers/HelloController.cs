using Microsoft.AspNetCore.Mvc;

namespace MvcStartApp.Controllers
{
    public class HelloController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}