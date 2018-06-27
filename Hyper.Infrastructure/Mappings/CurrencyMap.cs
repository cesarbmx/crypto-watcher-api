using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hyper.Domain.Models;

namespace Hyper.Infrastructure.Mappings
{
    public class CurrencyMap
    {
        public CurrencyMap(EntityTypeBuilder<Currency> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.Id);

            // Properties
            entityBuilder.Property(t => t.Rank)
                .IsRequired();

            entityBuilder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Rank)
                .IsRequired();

            entityBuilder.Property(t => t.MarketCap)
                .IsRequired();

            entityBuilder.Property(t => t.Price)
                .IsRequired();

            entityBuilder.Property(t => t.Volume24H)
                .IsRequired();
        }
    }
}
