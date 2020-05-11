using System;
using CesarBmx.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Types;

namespace CryptoWatcher.Persistence.Mappings
{
    public class AuditLogMap
    {
        public AuditLogMap(EntityTypeBuilder<AuditLog> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.AuditLogId)
                .IsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => t.CreatedAt)
                .IsClustered();

            // Properties
            entityBuilder.Property(t => t.AuditLogId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            entityBuilder.Property(t => t.Entity)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.EntityId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Action)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Json)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entityBuilder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Seed data
            var time = DateTime.Now;
            var user1 = new User("master", time);
            var indicator1 = new Indicator("price", IndicatorType.CurrencyIndicator, "master", "Price", "", "", null, 0, time);
            var indicator2 = new Indicator("price-change-24hrs", IndicatorType.CurrencyIndicator, "master", "Price change 24Hrs", "", "", null, 1, time);
            var indicator3 = new Indicator("hype", IndicatorType.CurrencyIndicator, "master", "Hype", "", "", null, 1, time);
            var indicatorDependencies1 = new IndicatorDependency("hype", "price-change-24hrs", time);

            entityBuilder.HasData(
                new AuditLog("Add", user1, user1.Id, time),
                new AuditLog("Add", indicator1, indicator1.Id, time),
                new AuditLog("Add", indicator2, indicator2.Id, time),
                new AuditLog("Add", indicator3, indicator3.Id, time),
                new AuditLog("Add", indicatorDependencies1, indicatorDependencies1.Id, time)
            );
        }
    }
}
