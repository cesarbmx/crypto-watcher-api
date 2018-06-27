using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using Hyper.Infrastructure.Contexts;

namespace Hyper.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly MainDbContext _mainDbContext;

        public CacheRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Cache> GetByKey(string key)
        {
            return await _mainDbContext.Cache.FirstOrDefaultAsync(x=>x.Key == key);
        }
        public void Add(Cache cache)
        {
            _mainDbContext.Cache.Add(cache);
        }
    }
}
