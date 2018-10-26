using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;
using Hyper.Domain.Services;

namespace Hyper.Persistence.Repositories
{
    public class CacheRepository: ICacheRepository
    {
        private readonly MainDbContext _mainDbContext;
        private readonly LogService _logService;

        public CacheRepository(MainDbContext mainDbContext, LogService logService)
        {
            _mainDbContext = mainDbContext;
            _logService = logService;
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

            // Log
            var log = new Log("Cache", "Add", cache);
            _logService.Log(log);
        }
    }
}
