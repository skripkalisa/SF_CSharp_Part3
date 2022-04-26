using System.Diagnostics;
using MakeFriends.Models;
using MakeFriends.Models.Users;
using MakeFriends.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MakeFriends.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;

  private readonly SignInManager<User> _signInManager;
  public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager)
  {
    _logger = logger;
    _signInManager = signInManager;
  }

  [Route("")]
  [Route("[controller]/[action]")]
  public IActionResult Index()
  {
    if (_signInManager.IsSignedIn(User)) 
    {
      return RedirectToAction("MyPage", "AccountManager");
    }

    return View();
    // return View(new MainViewModel());
  }

  [Route("[action]")]
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