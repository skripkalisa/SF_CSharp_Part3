using System.ComponentModel.DataAnnotations;

namespace MakeFriends.ViewModels.Account;

public class LoginViewModel
{
  [Required]
  [EmailAddress]
  [Display(Name = "Email", Prompt = "Введите email")]
  public string Email { get; set; } = default!;

  [Required]
  [DataType(DataType.Password)]
  [Display(Name = "Пароль", Prompt = "Введите пароль")]
  public string Password { get; set; } = default!;

  [Display(Name = "Запомнить?")] public bool RememberMe { get; set; }

  public string? ReturnUrl { get; set; }
}