using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities
{
    public class CacheTaskStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    public class CacheTaskStatusConfiguration : IEntityTypeConfiguration<CacheTaskStatus>
    {
        public void Configure(EntityTypeBuilder<CacheTaskStatus> builder)
        {
            builder
                .HasKey(s => s.Id);
            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(64);
            builder.HasIndex(s => s.Name).IsUnique();
        }
    }
}
