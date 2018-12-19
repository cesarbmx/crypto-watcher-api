using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Mappings
{
    public class LogMap
    {
        public LogMap(EntityTypeBuilder<Log> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.LogId);

            // Properties
            entityBuilder.Property(t => t.LogId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Entity)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            entityBuilder.Property(t => t.EntityId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            entityBuilder.Property(t => t.Action)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            entityBuilder.Property(t => t.Json)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entityBuilder.Property(t => t.CreatedBy)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CreationTime)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
