using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class IndicatorDependencyMap
    {
        public IndicatorDependencyMap(EntityTypeBuilder<IndicatorDependency> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => new { t.IndicatorId, t.DependencyId });

            // Relationships
            entityBuilder
                .HasOne(x => x.Indicator)
                .WithMany()
                .HasForeignKey(x => x.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(x => x.Dependency)
                .WithMany()
                .HasForeignKey(x => x.DependencyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Properties
            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.DependencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
