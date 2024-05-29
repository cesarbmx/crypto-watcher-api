using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using CesarBmx.Shared.Persistence.Extensions;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class UserLogMapping
    {
        public static void Map(this EntityTypeBuilder<UserLog> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.LogId)
                .IsClustered(false);

            // Indexes
            entityBuilder.HasIndex(t => new { t.UserId, t.CreatedAt })
                .IsUnique()
                .IsClustered();

            // Relationships
            entityBuilder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.LogId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.ActionType)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .HasStringToEnumConversion()
                .IsRequired();

            entityBuilder.Property(t => t.Description)
              .HasColumnType("nvarchar(250)")
              .HasMaxLength(250)
              .IsRequired();

            entityBuilder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
