using MakeFriends.Configs;
using MakeFriends.Models;
using MakeFriends.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MakeFriends.Data;

public sealed class ApplicationDbContext : IdentityDbContext<User>
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
  {
    Database.EnsureCreated();
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.ApplyConfiguration(new FriendConfiguration());
  }
}