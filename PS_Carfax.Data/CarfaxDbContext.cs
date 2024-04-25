using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PS_Carfax.Data.Models;
using PS_Carfax.UI.Services;

namespace PS_Carfax.Data
{
    public class CarfaxDbContext : DbContext
    {
            public DbSet<Vehicle> Vehicles { get; set; }
            public DbSet<ServiceRecord> ServiceRecords { get; set; }
            public DbSet<Owner> Owners { get; set; }
            public DbSet<History> Histories { get; set; }
            public DbSet<Accident> Accidents { get; set; }

            public CarfaxDbContext()
            {
                Database.EnsureCreated();
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<History>().HasMany(h => h.Owners);
            modelBuilder.Entity<History>().HasMany(h => h.Accidents);
            modelBuilder.Entity<History>().HasMany(h => h.ServiceRecords);
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                string solutionFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string databaseFile = "carfax.db";
                string databasePath = Path.Combine(solutionFolder, databaseFile);
                var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = databasePath };
                var connectionString = connectionStringBuilder.ToString();
                var connection = new SqliteConnection(connectionString);
                optionsBuilder.UseSqlite(connection);
            }
    }
}
