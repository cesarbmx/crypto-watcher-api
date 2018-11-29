using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class CurrencyMap
    {
        public CurrencyMap(EntityTypeBuilder<Currency> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.Id)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Rank)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.MarketCap)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.Price)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.Volume24H)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.PercentageChange24H)
                .HasColumnType("decimal")
                .IsRequired();
        }
    }
}
