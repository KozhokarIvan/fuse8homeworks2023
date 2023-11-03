using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Data
{
    public class PublicApiDbContext : DbContext
    {
        public PublicApiDbContext(DbContextOptions<PublicApiDbContext> options) : base(options)
        {

        }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<FavoriteExchange> FavoriteExchanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PublicApiDbContext).Assembly);
            modelBuilder.HasDefaultSchema("user");

            modelBuilder.Entity<Setting>().HasData(new Settings().Values);
            base.OnModelCreating(modelBuilder);
        }
    }
}
