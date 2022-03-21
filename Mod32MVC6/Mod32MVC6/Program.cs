using Microsoft.EntityFrameworkCore;
using Mod32MVC6.Middlewares;
using Mod32MVC6.Models;
using Mod32MVC6.Models.Db;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.
                       GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);
builder.Services.AddDbContext<BlogContext>( options =>
{
  options.UseSqlServer(connectionString);
});
builder.Services.AddRazorPages()
  .AddRazorRuntimeCompilation();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
// builder.Services.AddSingleton<IBlogRepository, BlogRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
// Подключаем логирвоание с использованием ПО промежуточного слоя
app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();