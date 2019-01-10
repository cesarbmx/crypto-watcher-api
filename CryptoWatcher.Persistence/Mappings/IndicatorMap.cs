using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class IndicatorMap
    {
        public IndicatorMap(EntityTypeBuilder<Indicator> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.IndicatorId).
                ForSqlServerIsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => new { t.Time, t.IndicatorType, t.IndicatorId, t.UserId})
                .ForSqlServerIsClustered();

            // Relationships
            entityBuilder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.IndicatorType)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(100)
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

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();

            // Seed data
            entityBuilder.HasData(
                new Indicator(IndicatorType.CurrencyIndicator, "price", "master", "Price", "", "", new List<IndicatorDependency>()),
                new Indicator(IndicatorType.CurrencyIndicator, "price-change-24hrs", "master", "Price change 24Hrs", "", "", new List<IndicatorDependency>()),
                new Indicator(IndicatorType.CurrencyIndicator, "hype", "master", "Hype", "", "", new List<IndicatorDependency>())
            );
        }
    }
}
