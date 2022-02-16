using CoreAppStart.Middlewares;
using CoreAppStart.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreAppStart
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private static IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CoreAppStartContext>(options => options.UseSqlServer(connection));
            // services.AddSingleton<UserInfoRepository>();
            services.AddScoped<IUserInfoRepository >();
            services.AddScoped<UserInfoRepository >();
            // services.AddSingleton<IUserInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()|| env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            // Console.WriteLine($"Launching project from: {env.ContentRootPath}");
            // Console.WriteLine($"wwwRootPath: {env.WebRootPath}");
            
            app.UseStaticFiles();
            
            app.UseRouting();
            

            
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