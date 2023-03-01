using System;
using System.Collections.Generic;
using CesarBmx.Shared.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class IndicatorMapping
    {
        public static void Map(this EntityTypeBuilder<Indicator> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.IndicatorId);

            // Indexes
            entityBuilder.HasIndex(t => new { t.UserId, t.Abbreviation })
                .IsUnique();

            // Relationships
            entityBuilder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Abbreviation)
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

            entityBuilder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Seed data
            var time = DateTime.UtcNow.StripSeconds();
            entityBuilder.HasData(
                new Indicator( "master", "PRICE", "Price", "", "", new List<IndicatorDependency>(), 0, time),
                new Indicator("master", "PRICE_CHANGE_24H",   "Price change 24Hrs", "", "", new List<IndicatorDependency>(), 1, time),
                new Indicator("master", "HYPE",   "Hype", "", "", new List<IndicatorDependency>(), 1, time)
            );
        }
    }
}
