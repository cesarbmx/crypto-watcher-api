using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Persistence.Repositories
{
    public class WatcherRepository : Repository<Watcher>, IWatcherRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WatcherRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<Watcher> GetByWatcherId(string watcherId)
        {
            return await _mainDbContext.Watchers.FirstOrDefaultAsync(x => x.WatcherId == watcherId);
        }

        public async Task<List<Watcher>> GetByUserId(string userId)
        {
            return await _mainDbContext.Watchers.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
