using FirstApp.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.DAL
{
    public sealed class AppContext : DbContext
    {
        // Объекты таблицы Users
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books{ get; set; }

        public AppContext()
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer( @"Server=localhost;Database=EF;User Id=SA;Password=51tuatIon;TrustServerCertificate=true;");
        }
    }
}