using Mod32MVC6.Models;

namespace Mod32MVC6.Middlewares;

public class LoggingMiddleware
{
  private readonly RequestDelegate _next;

  /// <summary>
  ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
  /// </summary>
  public LoggingMiddleware(RequestDelegate next)
  {
    _next = next;

  }

  
  /// <summary>
  ///  Необходимо реализовать метод Invoke  или InvokeAsync
  /// </summary>
  public async Task InvokeAsync(HttpContext context, IBlogRepository repo)
  {
    var url = $"https://{context.Request.Host.Value + context.Request.Path}";
    // Для логирования данных о запросе используем свойста объекта HttpContext
    // Console.WriteLine($"[{DateTime.Now}]: New request to https://{context.Request.Host.Value + context.Request.Path}");
    await repo.AddRequest(url);
    // Передача запроса далее по конвейеру
    await _next.Invoke(context);
  }
}