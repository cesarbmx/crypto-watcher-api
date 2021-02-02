using CesarBmx.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public static class LineMap
    {
        public static void Map(this EntityTypeBuilder<Line> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => new { t.Time, t.UserId, t.CurrencyId, t.IndicatorId });

            // Indexes
            entityBuilder.HasIndex(t => t.Time);
            entityBuilder.HasIndex(t => t.UserId);
            entityBuilder.HasIndex(t => t.CurrencyId);
            entityBuilder.HasIndex(t => t.IndicatorId);

            // Properties
            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            
            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            
            entityBuilder.Property(t => t.Value)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.AverageBuy)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.AverageSell)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Period)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .HasStringToEnumConversion()
                .IsRequired();
        }
    }
}
