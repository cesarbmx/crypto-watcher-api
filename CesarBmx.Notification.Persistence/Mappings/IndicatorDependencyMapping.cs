using System;
using CesarBmx.Shared.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.Notification.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.Notification.Persistence.Mappings
{
    public static class IndicatorDependencyMapping
    {
        public static void Map(this EntityTypeBuilder<IndicatorDependency> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t=> new {  t.IndicatorId, t.DependencyId});

            // Relationships
            entityBuilder
                .HasOne<Indicator>()
                .WithMany(x => x.Dependencies)
                .HasForeignKey(t => t.IndicatorId )
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(x=>x.Dependency)
                .WithMany()
                .HasForeignKey(t => t.DependencyId)
                .OnDelete(DeleteBehavior.NoAction);

            // Properties
            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.DependencyId)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(50)
                .IsRequired();

            // Seed data
            var time = DateTime.UtcNow.StripSeconds();
            entityBuilder.HasData(new IndicatorDependency("master.HYPE", "master.PRICE_CHANGE_24H", time));
        }
    }
}
