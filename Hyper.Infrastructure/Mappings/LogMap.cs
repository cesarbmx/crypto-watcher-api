using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hyper.Domain.Models;

namespace Hyper.Infrastructure.Mappings
{
    public class LogMap
    {
        public LogMap(EntityTypeBuilder<Log> entityBuilder)
        {
            // Key
            entityBuilder.HasKey(t => t.Id);

            // Properties
            entityBuilder.Property(t => t.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            entityBuilder.Property(t => t.LogLevelString)
                .HasColumnName("LogLevel")
                .HasColumnType("nvarchar")
                .IsRequired();

            entityBuilder.Property(t => t.Message)
                .HasColumnType("nvarchar(max)")
                .IsRequired();

            entityBuilder.Property(t => t.CreationTime)
                .HasColumnType("datetime")
                .IsRequired();

            entityBuilder.Ignore(x => x.LogLevel);
        }
    }
}
