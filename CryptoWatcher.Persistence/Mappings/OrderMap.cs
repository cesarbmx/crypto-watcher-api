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
            entityBuilder.HasKey(t => t.OrderId)
                .IsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => t.CreatedAt)
                .IsClustered();

            // Relationships
            entityBuilder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne<Currency>()
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.OrderId)
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityBuilder.Property(t => t.OrderType)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Quantity)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            entityBuilder.Property(t => t.OrderStatus)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.NotificationProcessedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            entityBuilder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
