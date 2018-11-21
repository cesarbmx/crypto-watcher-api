using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class WatcherMap
    {
        public WatcherMap(EntityTypeBuilder<Watcher> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.CurrentValue)
                .HasColumnType("decimal")
                .IsRequired();

            // Complex types
            entityBuilder.OwnsOne(t => t.Settings,
                p =>
                {
                    p.Property(t => t.BuyAt)
                        .HasColumnType("decimal")
                        .IsRequired();

                    p.Property(t => t.SellAt)
                        .HasColumnType("decimal")
                        .IsRequired();
                });

            entityBuilder.OwnsOne(t => t.TrendSettings,
                p =>
                {
                    p.Property(t => t.BuyAt)
                        .HasColumnType("decimal")
                        .IsRequired();

                    p.Property(t => t.SellAt)
                        .HasColumnType("decimal")
                        .IsRequired();
                });
        }
    }
}
