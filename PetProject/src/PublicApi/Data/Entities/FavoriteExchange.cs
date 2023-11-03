using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Entities
{
    public class FavoriteExchange
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string BaseCurrency { get; set; } = null!;
    }
    public class FavoriteExchangeConfiguration : IEntityTypeConfiguration<FavoriteExchange>
    {
        public void Configure(EntityTypeBuilder<FavoriteExchange> builder)
        {
            builder
                .HasKey(e => e.Id);
            builder
                .HasIndex(e => new { e.Currency, e.BaseCurrency })
                .IsUnique();
            builder
                .HasIndex(e => e.Name)
                .IsUnique();

            builder
                .Property(e => e.Name)
                .IsRequired();
            builder
                .Property(e => e.Currency)
                .IsRequired();
            builder
                .Property(e => e.BaseCurrency)
                .IsRequired();
        }
    }
}
