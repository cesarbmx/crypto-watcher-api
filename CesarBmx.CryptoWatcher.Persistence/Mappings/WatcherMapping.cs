﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using CesarBmx.Shared.Persistence.Extensions;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class WatcherMapping
    {
        public static void Map(this EntityTypeBuilder<Watcher> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.WatcherId )
                .IsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => new { t.UserId, t.CurrencyId, t.IndicatorId })
                .IsUnique()
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
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder
                .HasOne<Indicator>()
                .WithMany()
                .HasForeignKey(t =>  t.IndicatorId )
                .OnDelete(DeleteBehavior.Restrict);

            // Complex types
            entityBuilder.OwnsOne(t => t.BuyingOrder);
            entityBuilder.OwnsOne(t => t.SellingOrder);

            // Properties
            entityBuilder.Property(t => t.WatcherId)
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Status)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .HasStringToEnumConversion()
                .IsRequired();

            entityBuilder.Property(t => t.CurrencyId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.IndicatorId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Value)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Buy)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Sell)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Quantity)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.AverageBuy)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.AverageSell)
                .HasColumnType("decimal(18,2)");

            entityBuilder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)");          

            entityBuilder.Property(t => t.Enabled)
                .HasColumnType("bit")
                .IsRequired();

            entityBuilder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
