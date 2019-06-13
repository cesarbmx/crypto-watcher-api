using System;
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
                .WithMany(x=>x.Dependencies)
                .HasForeignKey(x => x.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne<Indicator>()
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

            // Seed data
            var time = DateTime.Now;
            entityBuilder.HasData(new IndicatorDependency("hype", "price-change-24hrs", time));
        }
    }
}
