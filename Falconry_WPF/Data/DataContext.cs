using Falconry_WPF.Models;
using Microsoft.EntityFrameworkCore;

namespace Falconry_WPF.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = DataFile.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public DbSet<Logbook> Logbooks { get; set; }
    }
}