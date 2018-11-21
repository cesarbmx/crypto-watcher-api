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
            entityBuilder.Property(t => t.Percentage)
                .HasColumnType("smallint")
                .IsRequired();

            entityBuilder.Property(t => t.Trend)
                .HasColumnType("smallint")
                .IsRequired();
        }
    }
}
