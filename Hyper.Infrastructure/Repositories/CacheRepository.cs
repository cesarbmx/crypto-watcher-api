using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;

namespace Hyper.Persistence.Repositories
{
    public class CacheRepository: ICacheRepository
    {
        private readonly MainDbContext _mainDbContext;

        public CacheRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Cache> GetByKey(string key)
        {
            // Get cache
            return await _mainDbContext.Cache.FirstOrDefaultAsync(CacheExpressions.HasKey(key));
        }
        public void Add(Cache cache)
        {
            // Add
            _mainDbContext.Cache.Add(cache);
        }
    }
}
