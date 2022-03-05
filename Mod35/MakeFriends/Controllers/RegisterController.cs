using MakeFriends.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace MakeFriends.Controllers;

public class RegisterController : Controller
{
  // public IActionResult Register()
  // {
  //   throw new NotImplementedException();
  // }
  //
  // public IActionResult RegisterPart2()
  // {
  //   throw new NotImplementedException();
  // }
  
  [Route("Register")]
  [HttpGet]
  public IActionResult Register()
  {
    return View("_Register");
    // return View("Home/Register");
  }

  [Route("RegisterPart2")]
  [HttpGet]
  public IActionResult RegisterPart2(RegisterViewModel model)
  {
    return View("_RegisterPart2", model);
    // return View("RegisterPart2", model);
  }
}