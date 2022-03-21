using Mod32MVC6.Models.Db;

namespace Mod32MVC6.Models;

public interface IBlogRepository
{       
  Task AddUser(User user);
  
}