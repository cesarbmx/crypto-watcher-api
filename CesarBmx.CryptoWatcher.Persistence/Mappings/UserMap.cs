﻿using System;
using CesarBmx.Shared.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CesarBmx.CryptoWatcher.Persistence.Mappings
{
    public static class UserMap
    {
        public static void Map(this EntityTypeBuilder<User> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.UserId);

            // Properties
            entityBuilder.Property(t => t.UserId)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.PhoneNumber)
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            entityBuilder.Property(t => t.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            // Data seeding
            var time = DateTime.UtcNow.StripSeconds();
            entityBuilder.HasData(
                new User("Master", "+34 666868686", time)
            );
        }
    }
}
