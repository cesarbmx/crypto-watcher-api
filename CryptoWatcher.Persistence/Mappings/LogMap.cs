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
            var user1 = new User("master");
            var indicator1 = new Indicator(IndicatorType.CurrencyIndicator, "price", "master", "Price", "", "", null);
            var indicator2 = new Indicator(IndicatorType.CurrencyIndicator, "price-change-24hrs", "master", "Price change 24Hrs", "", "", null);
            var indicator3 = new Indicator(IndicatorType.CurrencyIndicator, "hype", "master", "Hype", "", "", null);
            var indicatorDependencies1 = new IndicatorDependency("hype", "price-change-24hrs");

            entityBuilder.HasData(
                new Log("Add", user1, user1.Id),
                new Log("Add", indicator1, indicator1.Id),
                new Log("Add", indicator2, indicator2.Id),
                new Log("Add", indicator3, indicator3.Id),
                new Log("Add", indicatorDependencies1, indicatorDependencies1.Id)
            );
        }
    }
}
