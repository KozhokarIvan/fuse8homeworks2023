using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data
{
    public class InternalApiDbContext : DbContext
    {
        public InternalApiDbContext(DbContextOptions<InternalApiDbContext> options) : base(options) { }
        public DbSet<CurrencyCode> CurrencyCodes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CacheTaskStatus> CacheTaskStatuses { get; set; }
        public DbSet<CacheTask> CacheTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InternalApiDbContext).Assembly);
            modelBuilder.HasDefaultSchema("cur");
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<CurrencyCode>().HasData(new CurrencyCodes().Values);
            modelBuilder.Entity<CacheTaskStatus>().HasData(new CacheTaskStatuses().Values);
            base.OnModelCreating(modelBuilder);
        }
    }
}
