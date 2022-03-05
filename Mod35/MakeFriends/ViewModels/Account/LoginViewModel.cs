using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MakeFriends.ViewModels.Account;

public class LoginViewModel : IdentityUser
{ 
  [DataType(DataType.Password)]
  [Display(Name = "Пароль")]
  public string? Password { get; set; }

  public string? ReturnUrl { get; set; }
  [Display(Name = "RememberMe")]
  public bool RememberMe { get; set; }
}