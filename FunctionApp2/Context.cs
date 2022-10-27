using Microsoft.EntityFrameworkCore;

namespace FunctionApp2
{
    public class PowerConsumptionContext : DbContext
    {

        public PowerConsumptionContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PowerConsumption>()
                .ToTable("PowerConsumption", t => t.ExcludeFromMigrations());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PowerConsumption> PowerConsumption { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Host=powerconsumptiondatabaseserver.postgres.database.azure.com;Port=5432;Database=postgres;Username=PowerConsumptionAdministrator;Password=1J*325TYm$N1");
        }
    }
}