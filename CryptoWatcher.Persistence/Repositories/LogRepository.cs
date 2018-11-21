using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Repositories
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        private readonly MainDbContext _mainDbContext;

        public LogRepository(MainDbContext mainDbContext)
        : base(mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<List<Log>> GetFromDate(DateTime dateTime)
        {
            return await _mainDbContext.Log.Where(x => x.CreationTime < dateTime).ToListAsync();
        }
    }
}
