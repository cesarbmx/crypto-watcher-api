using Hyper.Domain.Models;
using Hyper.Persistence.Contexts;
using Hyper.Domain.Repositories;

namespace Hyper.Persistence.Repositories
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(MainDbContext mainDbContext) : base(mainDbContext)
        {
        }
    }
}
