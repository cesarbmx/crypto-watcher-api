using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class LineMap
    {
        public LineMap(EntityTypeBuilder<Line> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.LineId).
                ForSqlServerIsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => new { t.CurrencyId, t.IndicatorId });
            entityBuilder.HasIndex(t => new { t.Time, t.CurrencyId, t.IndicatorId })
                .ForSqlServerIsClustered();

            // Properties
            entityBuilder.Property(t => t.LineId)
                .HasColumnType("uniqueidentifier")
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
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entityBuilder.Property(t => t.AverageBuy)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.AverageSell)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
