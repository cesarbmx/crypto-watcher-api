using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class NotificationMap
    {
        public static void Map(this EntityTypeBuilder<Notification> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.NotificationId);

            // Relationships
            entityBuilder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            entityBuilder.Property(t => t.NotificationId)
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.PhoneNumber)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();
            
            entityBuilder.Property(t => t.Message)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(200)
                .IsRequired();

            entityBuilder.Property(t => t.SentTime)
                .HasColumnType("datetime2");

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();
        }
    }
}
