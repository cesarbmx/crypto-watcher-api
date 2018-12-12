using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class IndicatorMap
    {
        public IndicatorMap(EntityTypeBuilder<Indicator> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(1000)
                .IsRequired();

            entityBuilder.Property(t => t.Formula)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            // Data seeding
            entityBuilder.HasData(
                new Indicator("master", "price-change-24hrs", "", ""),
                new Indicator("master", "hype", "", "")
            );
        }
    }
}
