using System.Diagnostics;
using AutoMapper;
using MakeFriends.Data.Repository;
using MakeFriends.Data.UoW;
using MakeFriends.Extensions;
using MakeFriends.Models;
using MakeFriends.Models.Users;
using MakeFriends.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MakeFriends.Controllers;

public class AccountManagerController : Controller
{
  private readonly IMapper _mapper;

  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly IUnitOfWork _unitOfWork;

  public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper,
    IUnitOfWork unitOfWork)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
  }

  [Route("Login")]
  [HttpGet]
  public IActionResult Login()
  {
    return View();
    // return View("Home/Login");
  }

  [HttpGet]
  public IActionResult Login(string? returnUrl)
  {
    return View(new LoginViewModel { ReturnUrl = returnUrl });
  }


  [Route("Login")]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(LoginViewModel model)
  {
    if (ModelState.IsValid)
    {
      var user = _mapper.Map<User>(model);

      var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);
      // if (result.Succeeded)
      // {
      //   if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
      //   {
      //     return Redirect(model.ReturnUrl);
      //   }
      //
      //   return RedirectToAction("Index", "Home");
      // }
      if (result.Succeeded)
      {
        return RedirectToAction("MyPage", "AccountManager");
      }

      ModelState.AddModelError("", "Неправильный логин и (или) пароль");
    }

    return RedirectToAction("Index", "Home");
  }

  [Route("Logout")]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return RedirectToAction("Index", "Home");
  }

  [Route("UserList")]
  [HttpGet]
  public async Task<IActionResult> UserList(string search)
  {
    var model = await CreateSearch(search);
    return View("UserList", model);
  }

  private async Task<SearchViewModel> CreateSearch(string search)
  {
    var currentuser = User;

    var result = await _userManager.GetUserAsync(currentuser);

    var list = _userManager.Users.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(search.ToLower()))
      .ToList();
    var withfriend = await GetAllFriend();

    var data = new List<UserWithFriendExt>();
    list.ForEach(x =>
    {
      var t = _mapper.Map<UserWithFriendExt>(x);
      t.IsFriendWithCurrent = withfriend.Count(y => y.Id == x.Id || x.Id == result.Id) != 0;
      data.Add(t);
    });

    var model = new SearchViewModel
    {
      UserList = data
    };

    return model;
  }

  [Authorize]
  [Route("MyPage")]
  [HttpGet]
  public async Task<IActionResult> MyPage()
  {
    var user = User;

    var result = await _userManager.GetUserAsync(user);

    var model = new UserViewModel(result);

    model.Friends = await GetAllFriend(model.User);

    return View("User", model);
  }

  private async Task<List<User>> GetAllFriend(User user)
  {
    var repository =  _unitOfWork.GetRepository<Friend>() as FriendsRepository;

    Debug.Assert(repository != null, nameof(repository) + " != null");
    return repository.GetFriendsByUser(user);
  }

  private async Task<List<User>> GetAllFriend()
  {
    var user = User;

    var result = await _userManager.GetUserAsync(user);

    var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

    Debug.Assert(repository != null, nameof(repository) + " != null");
    return repository.GetFriendsByUser(result);
  }

  [Route("Edit")]
  [HttpGet]
  public IActionResult Edit()
  {
    var user = User;

    var result = _userManager.GetUserAsync(user);

    var editmodel = _mapper.Map<UserEditViewModel>(result.Result);

    return View("Edit", editmodel);
  }


  [Authorize]
  [Route("Update")]
  [HttpPost]
  public async Task<IActionResult> Update(UserEditViewModel model)
  {
    if (ModelState.IsValid)
    {
      var user = await _userManager.FindByIdAsync(model.UserId);

      user.Convert(model);

      var result = await _userManager.UpdateAsync(user);
      if (result.Succeeded)
      {
        return RedirectToAction("MyPage", "AccountManager");
      }
      else
      {
        return RedirectToAction("Edit", "AccountManager");
      }
    }
    else
    {
      ModelState.AddModelError("", "Некорректные данные");
      return View("Edit", model);
    }
  }

  [Route("AddFriend")]
  [HttpPost]
  public async Task<IActionResult> AddFriend(string id)
  {
    var currentuser = User;

    var result = await _userManager.GetUserAsync(currentuser);

    var friend = await _userManager.FindByIdAsync(id);

    var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

    repository?.AddFriend(result, friend);

    return RedirectToAction("MyPage", "AccountManager");
  }

  [Route("DeleteFriend")]
  [HttpPost]
  public async Task<IActionResult> DeleteFriend(string id)
  {
    var currentuser = User;

    var result = await _userManager.GetUserAsync(currentuser);

    var friend = await _userManager.FindByIdAsync(id);

    var repository = _unitOfWork.GetRepository<Friend>() as FriendsRepository;

    repository?.DeleteFriend(result, friend);

    return RedirectToAction("MyPage", "AccountManager");
  }

  [Route("Chat")]
  [HttpPost]
  public async Task<IActionResult> Chat(string id)
  {
    var model = await GenerateChat(id);
    return View("Chat", model);
  }

  private async Task<ChatViewModel> GenerateChat(string id)
  {
    var currentuser = User;

    var result = await _userManager.GetUserAsync(currentuser);
    var friend = await _userManager.FindByIdAsync(id);


    var repository = _unitOfWork.GetRepository<Message>() as MessageRepository;

    var mess = repository?.GetMessages(result, friend);

    Debug.Assert(mess != null, nameof(mess) + " != null");
    var model = new ChatViewModel
    {
      You = result,
      ToWhom = friend,
      History = mess.OrderBy(x => x.Id).ToList()
    };

    return model;
  }

  [Route("Chat")]
  [HttpGet]
  public async Task<IActionResult> Chat()
  {
    var id = Request.Query["id"];

    var model = await GenerateChat(id);
    return View("Chat", model);
  }

  [Route("NewMessage")]
  [HttpPost]
  public async Task<IActionResult> NewMessage(string id, ChatViewModel chat)
  {
    var currentuser = User;

    var result = await _userManager.GetUserAsync(currentuser);
    var friend = await _userManager.FindByIdAsync(id);

    var repository = _unitOfWork.GetRepository<Message>() as MessageRepository;

    var item = new Message
    {
      Sender = result,
      Recipient = friend,
      Text = chat.NewMessage.Text,
    };
    repository?.Create(item);

    var model = await GenerateChat(id);
    return View("Chat", model);
  }
  // public IActionResult UserList()
  // {
  //   return Content("search");
  // }
}