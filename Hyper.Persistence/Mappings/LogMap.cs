using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hyper.Domain.Models;

namespace Hyper.Persistence.Mappings
{
    public class LogMap
    {
        public LogMap(EntityTypeBuilder<Log> entityBuilder)
        {
            // Properties
            entityBuilder.Property(t => t.Resource)
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
