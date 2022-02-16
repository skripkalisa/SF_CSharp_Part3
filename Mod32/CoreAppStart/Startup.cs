using System;
using System.IO;
using CoreAppStart.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreAppStart
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private static IWebHostEnvironment _env;

        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _env = env;
            if (env.IsDevelopment()|| env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            // Console.WriteLine($"Launching project from: {env.ContentRootPath}");
            // Console.WriteLine($"wwwRootPath: {env.WebRootPath}");
            
            app.UseStaticFiles();
            
            app.UseRouting();
            
            //Используем метод Use, чтобы запрос передавался дальше по конвейеру
            // app.Use(async (context, next) =>
            // {
            //     // Строка для публикации в лог
            //     string logMessage = $"[{DateTime.Now}]: New request to https://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
            //
            //     // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            //     string logFilePath = Path.Combine(env.ContentRootPath, "Logs", "RequestLog.txt");
            //
            //     // Используем асинхронную запись в файл
            //     await File.AppendAllTextAsync(logFilePath, logMessage);
            //
            //     await next.Invoke();
            // });
            
            //Добавляем компонент для логирования запросов с использованием метода Use.
            // app.Use(async (context, next) =>
            // {
            //     // Для логирования данных о запросе используем свойста объекта HttpContext
            //     Console.WriteLine($"[{DateTime.Now}]: New request to https://{context.Request.Host.Value + context.Request.Path}");
            //     await next.Invoke();
            // });
            
            app.UseMiddleware<LoggingMiddleware>();
            // Поддержка статических файлов
//Добавляем компонент с настройкой маршрутов для главной страницы
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
                });
            });
  
            // Все прочие страницы имеют отдельные обработчики
            app.Map("/about", About);
            app.Map("/config", Config);
  
            // Обработчик для ошибки "страница не найдена"
// обрабатываем ошибки HTTP
                app.UseStatusCodePages();
            // app.Run(async (context) =>
            // {
            //     // int zero = 0;
            //     // int result = 4 / zero;
            //     await context.Response.WriteAsync($"Page not found");
            // });
            //

        
        }
        /// <summary>
        ///  Обработчик для страницы About
        /// </summary>
        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{_env.ApplicationName} - ASP.Net Core tutorial project");
            });
        }
 
        /// <summary>
        ///  Обработчик для главной страницы
        /// </summary>
        private static void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {_env.ApplicationName}. App running configuration: {_env.EnvironmentName}");
            });
        }
    }
}
