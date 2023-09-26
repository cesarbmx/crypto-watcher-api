using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class OrderMapping
    {
        public static void Map(this EntityTypeBuilder<Order> entityBuilder)
        {

            // Properties
            entityBuilder.Property(t => t.OrderId)
                   .HasColumnType("uniqueidentifier")
                   .IsRequired();

            entityBuilder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                   .IsRequired();

            entityBuilder.Property(t => t.ExecutedAt)
                .HasColumnType("datetime2")
                   .IsRequired();
        }
    }
}
