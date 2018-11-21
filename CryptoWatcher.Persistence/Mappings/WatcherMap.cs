using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class WatcherMap
    {
        public WatcherMap(EntityTypeBuilder<Watcher> entityBuilder)
        {
            // Complex types
            entityBuilder.OwnsOne(t => t.BuySellSettings,
                sa =>
                {
                    // Properties
                    sa.Property(p => p.CurrencyId)
                        .HasColumnType("nvarchar")
                        .HasMaxLength(50)
                        .IsRequired();

                    sa.Property(p => p.BuyAt)
                        .HasColumnType("decimal")
                        .IsRequired();

                    sa.Property(p => p.SellAt)
                        .HasColumnType("decimal")
                        .IsRequired();
                });

            entityBuilder.OwnsOne(t => t.TrendSettings,
                sa =>
                {
                    // Properties
                    sa.Property(p => p.CurrencyId)
                        .HasColumnType("nvarchar")
                        .HasMaxLength(50)
                        .IsRequired();

                    sa.Property(p => p.BuyAt)
                        .HasColumnType("decimal")
                        .IsRequired();

                    sa.Property(p => p.SellAt)
                        .HasColumnType("decimal")
                        .IsRequired();
                });
        }
    }
}
