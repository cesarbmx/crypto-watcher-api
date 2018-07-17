using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Infrastructure.Contexts;
using System.Collections.Generic;

namespace Hyper.Infrastructure.Repositories
{
    public class CacheRepository
    {
        private readonly MainDbContext _mainDbContext;

        public CacheRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<IEnumerable<T>> GetByKey<T>()
        {
            // Get cache
            var cache = await _mainDbContext.Cache.FirstOrDefaultAsync(x=>x.Key == typeof(T).Name);

            // Return
            if (cache == null) return new List<T>();
            return cache.GetValue<T>();
        }
        public async Task Set<T>(IEnumerable<T> t)
        {
            var currentCache = await _mainDbContext.Cache.FirstOrDefaultAsync(x => x.Key == typeof(T).Name);
            if (currentCache != null)
            {
                // Set value
                currentCache.SetValue(t);
            }
            else
            {
                // Set cache
                var cache = new Cache();
                cache.SetValue(t);
            }
        }
    }
}
