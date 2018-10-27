using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Expressions;
using Hyper.Domain.Repositories;
using System.Linq;

namespace Hyper.Persistence.AuditRepositories
{
    public class CacheAuditRepository : AuditRepository<Cache>
    {
        public CacheAuditRepository(ILogRepository logRepository):base(logRepository)
        {}

        public Task<Cache> GetByKey(string key)
        {
            // Get cache
            return Task.FromResult(List.FirstOrDefault(CacheExpressions.HasKey(key).Compile()));
        }

    }
}
