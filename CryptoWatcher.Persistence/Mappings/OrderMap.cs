using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class OrderMap
    {
        public OrderMap(EntityTypeBuilder<Order> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.OrderId);

            // Properties
            entityBuilder.Property(t => t.OrderId)
                .HasColumnType("nvarchar(400)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.UserId)
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50)
                    .IsRequired();

            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.WatcherId)
                .HasColumnType("nvarchar(200)")
                .HasMaxLength(200)
                .IsRequired();

            entityBuilder.Property(t => t.OrderType)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Quantity)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.Status)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.CreatedBy)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
