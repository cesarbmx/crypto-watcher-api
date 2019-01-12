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
                .ForSqlServerIsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => t.Time)
                .ForSqlServerIsClustered();

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
                .HasColumnType("uniqueidentifier")
                .IsRequired();

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

            entityBuilder.Property(t => t.WatcherId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Quantity)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            entityBuilder.Property(t => t.Status)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
