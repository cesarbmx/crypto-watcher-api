using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoWatcher.Domain.Models;

namespace CryptoWatcher.Persistence.Mappings
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            // Data seeding
            entityBuilder.HasData(
                new User("master")
            );
        }
    }
}
