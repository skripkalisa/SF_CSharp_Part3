using Microsoft.AspNetCore.Mvc;
using Mod32MVC6.Models;
using Mod32MVC6.Models.Db;

namespace Mod32MVC6.Controllers;

public class UsersController : Controller
{
  private readonly IBlogRepository _repo;
      
  public UsersController(IBlogRepository repo)
  {
    _repo = repo;
  }
      
  public async Task <IActionResult> Index()
  {
    var authors = await _repo.GetUsers();
    return View(authors);
  }
  
  [HttpGet]
  public IActionResult Register()
  {
    return View();
  }
  
  [HttpPost]
  public async Task<IActionResult> Register(User user)
  {
    await _repo.AddUser(user);
    // ViewData["Message"] = $"Registration successful, {user.FirstName}";
    return View(user);
    // return Content($"Registration successful, {user.FirstName}");
  }
         
         
  
}