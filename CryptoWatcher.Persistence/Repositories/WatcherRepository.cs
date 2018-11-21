using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;

namespace CryptoWatcher.Persistence.Repositories
{
    public class WatcherRepository : Repository<Watcher>, IWatcherRepository
    {
        public WatcherRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        {
        }
    }
}
