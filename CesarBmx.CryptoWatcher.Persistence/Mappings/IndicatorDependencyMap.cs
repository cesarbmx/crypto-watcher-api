using System;
using CesarBmx.Shared.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class IndicatorDependencyMap
    {
        public static void Map(this EntityTypeBuilder<IndicatorDependency> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t=> new { t.UserId, t.IndicatorId, t.DependencyUserId, t.DependencyIndicatorId});

            // Relationships
            entityBuilder
                .HasOne<Indicator>()
                .WithMany(x => x.Dependencies)
                .HasForeignKey(t => new { t.UserId, t.IndicatorId })
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne<Indicator>()
                .WithMany()
                .HasForeignKey(t => new { t.DependencyUserId, t.DependencyIndicatorId })
                .OnDelete(DeleteBehavior.Restrict);

            // Properties
            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.DependencyUserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.DependencyIndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            // Seed data
            var time = DateTime.UtcNow.StripSeconds();
            entityBuilder.HasData(new IndicatorDependency("Master", "HYPE", "Master", "PRICE_CHANGE_24hrs", time));
        }
    }
}
