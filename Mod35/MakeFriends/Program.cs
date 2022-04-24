using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MakeFriends.Data;
using MakeFriends.Models.Users;
using MakeFriends.Data.Repository;
using AutoMapper;
using MakeFriends;
using MakeFriends.Extensions;
using MakeFriends.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
  .AddRazorRuntimeCompilation();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddUnitOfWork();
builder.Services.AddCustomRepository<Friend, FriendsRepository>();
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
// .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.Password.RequiredLength = 5;   
    opts.Password.RequireNonAlphanumeric = false;  
    opts.Password.RequireLowercase = false; 
    opts.Password.RequireUppercase = false; 
    opts.Password.RequireDigit = false;
  })
  .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var mapperConfig = new MapperConfiguration((v) =>
{
  v.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

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

