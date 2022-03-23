using Microsoft.AspNetCore.Mvc;
using Mod32MVC6.Models;

namespace Mod32MVC6.Controllers;

public class LogsController : Controller
{
    private readonly IBlogRepository _repo;
    
    public LogsController(IBlogRepository repo)
    {
        _repo = repo;
    }
  //     public IActionResult Index()
  // {
  //   return View();
  // }
        public async Task <IActionResult> Index()
  {
    var requests = await _repo.GetRequests();
    return View(requests);
  }
}