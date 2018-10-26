using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;

namespace Hyper.Persistence.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly MainDbContext _mainDbContext;

        public LogRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Cache> GetByKey(string key)
        {
            // Get cache
            return await _mainDbContext.Cache.FirstOrDefaultAsync(CacheExpressions.HasKey(key));
        }

        public async Task<IEnumerable<Log>> GetLog()
        {
            return await _mainDbContext.Log.ToListAsync();
        }

        public void Add(Log log)
        {
            // Add
            _mainDbContext.Log.Add(log);
        }
    }
}
