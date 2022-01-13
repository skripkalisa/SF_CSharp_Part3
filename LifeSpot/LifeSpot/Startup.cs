using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LifeSpot
{
   public class Startup
   {
       public void ConfigureServices(IServiceCollection services)
       {
       }
 
       public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
       {
           if (env.IsDevelopment())
               app.UseDeveloperExceptionPage();
 
           app.UseRouting();
         
           // Загружаем отдельные элементы для вставки в шаблон: боковое меню и футер
           string headerHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "header.html"));
           string footerHtml =
               File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "footer.html"));
           string sideBarHtml =  File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sidebar.html"));
 
           app.UseEndpoints(endpoints =>
           {
               endpoints.MapGet("/", async context =>
               {
                   var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "index.html");
                  
                   // Загружаем шаблон страницы, вставляя в него элементы
                   var html =  new StringBuilder(await File.ReadAllTextAsync(viewPath))
                       .Replace("<!--HEADER-->", headerHtml)
                       .Replace("<!--SIDEBAR-->", sideBarHtml)
                       .Replace("<!--FOOTER-->", footerHtml);
                  
                   await context.Response.WriteAsync(html.ToString());
               });
              
               endpoints.MapGet("/testing", async context =>
               {
                   var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "testing.html");
                  
                   // Загружаем шаблон страницы, вставляя в него элементы
                   var html =  new StringBuilder(await File.ReadAllTextAsync(viewPath))
                       .Replace("<!--HEADER-->", headerHtml)
                       .Replace("<!--SIDEBAR-->", sideBarHtml)
                       .Replace("<!--FOOTER-->", footerHtml);
                  
                   await context.Response.WriteAsync(html.ToString());
               });
              
               endpoints.MapGet("/Static/CSS/style.css", async context =>
               {
                   var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "CSS", "style.css");
                   var css = await File.ReadAllTextAsync(cssPath);
                   await context.Response.WriteAsync(css);
               });
              
               endpoints.MapGet("/Static/JS/app.js", async context =>
               {
                   var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", "app.js");
                   var js = await File.ReadAllTextAsync(jsPath);
                   await context.Response.WriteAsync(js);
               });               
               endpoints.MapGet("/Static/JS/testing.js", async context =>
               {
                   var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", "testing.js");
                   var js = await File.ReadAllTextAsync(jsPath);
                   await context.Response.WriteAsync(js);
               });
           });
       }
   }
}