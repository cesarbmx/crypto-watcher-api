using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Infrastructure.Contexts;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;

namespace Hyper.Infrastructure.Repositories
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
