using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class CurrencyMap
    {
        public static void Map(this EntityTypeBuilder<Currency> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.CurrencyId);

            // Properties
            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Rank)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Name)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.MarketCap)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.Volume24H)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.PercentageChange24H)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
