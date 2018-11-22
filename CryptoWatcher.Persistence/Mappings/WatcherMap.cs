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
            entityBuilder.Property(t => t.WatcherId)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.WatcherType)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyPrice)
                .HasColumnType("decimal")
                .IsRequired();

            // Complex types
            entityBuilder.OwnsOne(t => t.WatcherSettings,
                p =>
                {
                    p.Property(t => t.BuyAt)
                        .HasColumnType("decimal")
                        .IsRequired();

                    p.Property(t => t.SellAt)
                        .HasColumnType("decimal")
                        .IsRequired();
                });

            entityBuilder.OwnsOne(t => t.WatcherSettingsTrend,
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
