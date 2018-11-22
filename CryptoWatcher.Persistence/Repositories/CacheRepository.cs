using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Repositories
{
    public class CacheRepository : Repository<Cache>, ICacheRepository
    {
        private readonly MainDbContext _mainDbContext;

        public CacheRepository(MainDbContext mainDbContext) 
            : base(mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Cache> GetByKey(string key)
        {
            return await _mainDbContext.Caches.FirstOrDefaultAsync(x => x.Key == key);
        }
    }
}
