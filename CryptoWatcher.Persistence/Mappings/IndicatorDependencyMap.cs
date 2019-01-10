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
            entityBuilder.HasKey(t=> new { t.IndicatorId, t.DependsOn});

            // Relationships
            entityBuilder
                .HasOne<Indicator>()
                .WithMany(x=>x.Dependencies)
                .HasForeignKey(x => x.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne<Indicator>()
                .WithMany()
                .HasForeignKey(x => x.DependsOn)
                .OnDelete(DeleteBehavior.Restrict);

            // Properties
            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(t => t.DependsOn)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(100)
                .IsRequired();

            entityBuilder.Property(t => t.Level)
                .HasColumnType("smallint");

            // Seed data
            entityBuilder.HasData(new IndicatorDependency("hype", "price-change-24hrs"));
        }
    }
}
