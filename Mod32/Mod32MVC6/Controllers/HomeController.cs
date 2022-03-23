using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mod32MVC6.Models;
using Mod32MVC6.Models.Db;

namespace Mod32MVC6.Controllers;

public class HomeController : Controller
{
// ссылка на репозиторий
  private readonly IBlogRepository _repo;
  private readonly ILogger<HomeController> _logger;
 
// Также добавим инициализацию в конструктор
  public HomeController(ILogger<HomeController> logger, IBlogRepository repo)
  {
    _logger = logger;
    _repo = repo;
  }

  public IActionResult Index()
  {
    return View();
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}