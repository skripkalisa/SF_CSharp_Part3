using Microsoft.AspNetCore.Mvc;

namespace MvcStartApp.Controllers
{
    public class HelloController : Controller
    {
        // GET
        public string Index()
        {
            return "fuck!";
        }        
        public IActionResult Hello()
        {
            return View();
        }
    }
}