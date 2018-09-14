using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Infrastructure.Contexts;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;

namespace Hyper.Infrastructure.Repositories
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

        public async Task<List<Log>> GetLog()
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
