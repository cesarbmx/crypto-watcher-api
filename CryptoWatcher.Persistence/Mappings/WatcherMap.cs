using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public static class WatcherMap
    {
        public static void Map(this EntityTypeBuilder<Watcher> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.WatcherId)
                .IsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => new { t.UserId, t.IndicatorType, t.CurrencyId, t.IndicatorId })
                .IsClustered();

            // Relationships
            entityBuilder
                .HasOne<User>()
                .WithMany(x=>x.Watchers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.WatcherId)
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.IndicatorType)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Value)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Buy)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Sell)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.AverageBuy)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.AverageSell)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Enabled)
                .HasColumnType("bit")
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
