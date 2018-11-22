using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class CurrencyMap
    {
        public CurrencyMap(EntityTypeBuilder<Currency> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.CurrencyId);

            // Properties
            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyRank)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyName)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyMarketCap)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyPrice)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyVolume24H)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyPercentageChange24H)
                .HasColumnType("decimal")
                .IsRequired();
        }
    }
}
