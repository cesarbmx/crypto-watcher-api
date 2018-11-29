using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class OrderMap
    {
        public OrderMap(EntityTypeBuilder<Order> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.Id)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Id)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Quantity)
                .HasColumnType("decimal")
                .IsRequired();

            entityBuilder.Property(t => t.Status)
                .HasColumnType("smallint")
                .IsRequired();
        }
    }
}
