using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;

namespace CryptoWatcher.Persistence.Repositories
{
    public class CacheRepository : Repository<Cache>, ICacheRepository
    {
        public CacheRepository(MainDbContext mainDbContext) 
            : base(mainDbContext)
        {
        }
    }
}
