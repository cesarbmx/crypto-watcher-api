using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Repositories;

namespace Hyper.Persistence.Repositories
{
    public class CacheRepository : Repository<Cache>, ICacheRepository
    {
        public CacheRepository(MainDbContext mainDbContext) 
            : base(mainDbContext)
        {
        }
    }
}
