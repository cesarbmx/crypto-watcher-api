using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hyper.Domain.Models;

namespace Hyper.Persistence.Mappings
{
    public class CacheMap
    {
        public CacheMap(EntityTypeBuilder<Cache> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.Key);

            // Properties
            entityBuilder.Property(t => t.Key)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Value)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entityBuilder.Property(t => t.CreationTime)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
