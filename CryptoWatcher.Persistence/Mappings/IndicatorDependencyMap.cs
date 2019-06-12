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
            entityBuilder.HasKey(t=> new { t.IndicatorId, t.DependencyId});

            // Relationships
            entityBuilder
                .HasOne<Indicator>()
                .WithMany()
                .HasForeignKey(x => x.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne<Indicator>()
                .WithMany()
                .HasForeignKey(x => x.DependencyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(t => t.DependencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
