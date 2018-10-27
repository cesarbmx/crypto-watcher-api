using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;
using System.Collections.Generic;

namespace Hyper.Persistence.Repositories
{
    public class CacheRepository: ICacheRepository
    {
        private readonly MainDbContext _mainDbContext;

        public CacheRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }


        public async Task<List<Cache>> GetAll()
        {
            // Get all caches
            return await _mainDbContext.Cache.ToListAsync();
        }
        public async Task<Cache> GetById(int id)
        {
            // Get cache
            return await _mainDbContext.Cache.FirstOrDefaultAsync(x=>x.Id == id);
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
