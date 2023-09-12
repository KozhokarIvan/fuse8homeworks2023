using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities
{
    public class Currency
    {
        public CurrencyCode CurrencyCode { get; set; } = null!;
        public int CurrencyCodeId { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTime { get; set; }
    }
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder
                .HasKey(c => new { c.DateTime, c.CurrencyCodeId });

            builder
                .HasOne(c => c.CurrencyCode)
                .WithMany();
        }
    }
}
