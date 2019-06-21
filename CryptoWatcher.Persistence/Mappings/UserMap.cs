using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Mappings
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.UserId);

            // Properties
            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.Time)
                .HasColumnType("datetime2")
                .IsRequired();

            // Data seeding
            var time = DateTime.Now;
            entityBuilder.HasData(
                new User("master", time)
            );
        }
    }
}
