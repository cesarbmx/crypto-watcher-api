using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Mappings
{
    public class LogMap
    {
        public LogMap(EntityTypeBuilder<Log> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.Entity)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            entityBuilder.Property(t => t.Action)
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            entityBuilder.Property(t => t.Json)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }
}
