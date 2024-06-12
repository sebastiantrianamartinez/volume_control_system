using Microsoft.EntityFrameworkCore;

namespace cowhub
{
    public class CowhubDbContext : DbContext
    {
        public DbSet<Cow> Cows { get; set; }
        public DbSet<Production> Productions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=cowhub;User=root;Password=;",
                new MySqlServerVersion(new Version(8, 0, 21))); // Ajusta la versión según tu instalación de MySQL
        }
    }
}
