using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Mappings
{
    public class CacheMap
    {
        public CacheMap(EntityTypeBuilder<Cache> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.Value)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            // Data seeding
            entityBuilder.HasData(
                new Cache().SetValue(new List<Currency>()),
                new Cache().SetValue(new List<HistoricalData>())
            );
        }
    }
}
