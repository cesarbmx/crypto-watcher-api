using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class HypeMap
    {
        public HypeMap(EntityTypeBuilder<Hype> entityBuilder)
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
