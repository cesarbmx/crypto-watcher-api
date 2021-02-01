using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public static class IndicatorMap
    {
        public static void Map(this EntityTypeBuilder<Indicator> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.IndicatorId);

            // Indexes
            entityBuilder.HasIndex(t => new { t.UserId, t.IndicatorId })
                .IsUnique();

            // Relationships
            entityBuilder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Name)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Description)
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000)
                .IsRequired();

            entityBuilder.Property(t => t.Formula)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entityBuilder.Property(t => t.DependencyLevel)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();

            // Seed data
            var time = DateTime.Now;
            entityBuilder.HasData(
                new Indicator("price", "master", "Price", "", "", new List<IndicatorDependency>(), 0, time),
                new Indicator("price-change-24hrs",  "master", "Price change 24Hrs", "", "", new List<IndicatorDependency>(), 1, time),
                new Indicator("hype",  "master", "Hype", "", "", new List<IndicatorDependency>(), 1, time)
            );
        }
    }
}
