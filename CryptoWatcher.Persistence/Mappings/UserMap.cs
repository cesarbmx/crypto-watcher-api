using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Mappings
{
    public class UserMap
    {
        // ReSharper disable once UnusedParameter.Local
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            // Properties
            //entityBuilder.Property(t => t.Id)
            //    .HasColumnType("nvarchar")
            //    .HasMaxLength(50)
            //    .IsRequired();
        }
    }
}
