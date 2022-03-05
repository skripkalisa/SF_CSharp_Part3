using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MakeFriends.Data;
using MakeFriends.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
      options.Password.RequiredLength = 5;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireDigit = false;
      options.SignIn.RequireConfirmedAccount = true;
    }
  
  )
  .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
// builder.Services.AddIdentity<User, IdentityRole>()
//         .AddEntityFrameworkStores<ApplicationDbContext>()
//         .AddDefaultTokenProviders()
//         .AddDefaultUI();
// builder.Services.AddIdentity<User, IdentityRole>(opts => {
//     opts.Password.RequiredLength = 5;   
//     opts.Password.RequireNonAlphanumeric = false;  
//     opts.Password.RequireLowercase = false; 
//     opts.Password.RequireUppercase = false; 
//     opts.Password.RequireDigit = false;
//   })

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();