using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Mappings
{
    public class LogMap
    {
        public LogMap(EntityTypeBuilder<Log> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.LogId)
                .ForSqlServerIsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => t.Time)
                .ForSqlServerIsClustered();

            // Properties
            entityBuilder.Property(t => t.LogId)
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

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();

            // Seed data
            var time = DateTime.Now;
            var user1 = new User("master", time);
            var indicator1 = new Indicator("price", IndicatorType.CurrencyIndicator, "master", "Price", "", "", null, time);
            var indicator2 = new Indicator("price-change-24hrs", IndicatorType.CurrencyIndicator, "master", "Price change 24Hrs", "", "", null, time);
            var indicator3 = new Indicator("hype", IndicatorType.CurrencyIndicator, "master", "Hype", "", "", null, time);
            var indicatorDependencies1 = new IndicatorDependency("hype", "price-change-24hrs", 0, time);

            entityBuilder.HasData(
                new Log("Add", user1, user1.Id, time),
                new Log("Add", indicator1, indicator1.Id, time),
                new Log("Add", indicator2, indicator2.Id, time),
                new Log("Add", indicator3, indicator3.Id, time),
                new Log("Add", indicatorDependencies1, indicatorDependencies1.Id, time)
            );
        }
    }
}
