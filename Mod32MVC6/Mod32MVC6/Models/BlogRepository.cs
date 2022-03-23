using Microsoft.EntityFrameworkCore;
using Mod32MVC6.Models.Db;

namespace Mod32MVC6.Models;

public class BlogRepository : IBlogRepository
{
  // ссылка на контекст
  private readonly BlogContext _context;
  
  // Метод-конструктор для инициализации
  public BlogRepository(BlogContext context)
  {
    _context = context;
  }
  
  public async Task AddUser(User user)
  {
    user.JoinDate = DateTime.Now;
    user.Id = Guid.NewGuid();
 
    // Добавление пользователя
    var entry = _context.Entry(user);
    if (entry.State == EntityState.Detached)
      await _context.Users.AddAsync(user);
  
    // Сохранение изенений
    await _context.SaveChangesAsync();
  }  
  public async Task AddRequest(string url)
  {
    var request = new Request
    {
      Date = DateTime.Now,
      Id = Guid.NewGuid(),
      Url = url
    };

    // Добавление пользователя
    var entry = _context.Entry(request);
    if (entry.State == EntityState.Detached)
      if (_context.Requests != null)
        await _context.Requests.AddAsync(request);

    // Сохранение изенений
    await _context.SaveChangesAsync();
  }

  public async Task<User[]> GetUsers()
{
   // Получим всех активных пользователей
   return await _context.Users.ToArrayAsync();
}  
  public async Task<Request[]> GetRequests()
{
   // Получим всех активных пользователей
   return await _context.Requests.ToArrayAsync();
}
  
}