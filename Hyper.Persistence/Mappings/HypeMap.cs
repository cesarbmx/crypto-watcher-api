using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hyper.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hyper.Persistence.Mappings
{
    public class HypeMap
    {
        public HypeMap(EntityTypeBuilder<Hype> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.Id);

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
