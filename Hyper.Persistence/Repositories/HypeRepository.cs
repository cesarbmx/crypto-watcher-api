using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Repositories;

namespace Hyper.Persistence.Repositories
{
    public class HypeRepository : Repository<Hype>, IHypeRepository
    {
        public HypeRepository(MainDbContext mainDbContext)
            : base(mainDbContext)
        {
        }
    }
}
