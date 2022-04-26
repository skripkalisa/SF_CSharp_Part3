namespace MakeFriends.ViewModels.Account;

public class MainViewModel
{
  private RegisterViewModel RegisterView { get; set; }

  private LoginViewModel LoginView { get; set; }

  public MainViewModel()
  {
    RegisterView = new RegisterViewModel();
    LoginView = new LoginViewModel();
  }
}