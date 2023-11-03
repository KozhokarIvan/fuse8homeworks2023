using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities
{
    public class CurrencyCode
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    public class CurrencyCodeConfiguration : IEntityTypeConfiguration<CurrencyCode>
    {
        public void Configure(EntityTypeBuilder<CurrencyCode> builder)
        {
            builder
                .HasKey(cc => cc.Id);
            builder
                .Property(cc => cc.Name)
                .HasMaxLength(16)
                .IsRequired();
        }
    }
}
