using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoWatcher.Persistence.Mappings
{
    public class EntityMap
    {
        public EntityMap(EntityTypeBuilder<Entity> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.Id);

            // Properties
            entityBuilder.Property(t => t.Id)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CreationTime)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
