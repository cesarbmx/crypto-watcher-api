using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class LineMap
    {
        public LineMap(EntityTypeBuilder<Line> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Value)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
