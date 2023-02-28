using CesarBmx.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class EventMapping
    {
        public static void Map(this EntityTypeBuilder<Event> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.EventId);

            // Properties
            entityBuilder.Property(t => t.EventId)
                .HasColumnType("nvarchar(max)")
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityBuilder.Property(t => t.EventType)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .HasStringToEnumConversion()
                .IsRequired();

            entityBuilder.Property(t => t.Json)
                .HasColumnType("nvarchar(max)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
