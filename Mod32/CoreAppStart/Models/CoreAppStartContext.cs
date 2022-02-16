using Microsoft.EntityFrameworkCore;

namespace CoreAppStart.Models
{
    public sealed class CoreAppStartContext : DbContext
    {
        public DbSet<UserInfo> UserInfos { get; set; }
        
        public CoreAppStartContext(DbContextOptions<CoreAppStartContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserInfo>().ToTable("UserInfos");
        }
    }
}