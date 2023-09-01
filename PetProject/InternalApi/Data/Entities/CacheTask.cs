using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities
{
    public class CacheTask
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NewBaseCurrency { get; set; } = null!;
        public CacheTaskStatus Status { get; set; } = null!;
        public int StatusId { get; set; }
    }
    public class CacheTaskConfiguration : IEntityTypeConfiguration<CacheTask>
    {
        public void Configure(EntityTypeBuilder<CacheTask> builder)
        {
            builder
                .HasKey(t => t.Id);
            builder
                .Property(t => t.Id)
                .HasDefaultValueSql("uuid_generate_v4()");
            builder
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId);
            builder
                .Property(t => t.CreatedAt).IsRequired();
            builder
                .Property(t => t.NewBaseCurrency)
                .IsRequired();
        }
    }
}
