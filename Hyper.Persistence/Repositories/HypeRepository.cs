using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Domain.Repositories;

namespace CryptoWatcher.Persistence.Repositories
{
    public class HypeRepository : Repository<Hype>, IHypeRepository
    {
        public HypeRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        {
        }
    }
}
