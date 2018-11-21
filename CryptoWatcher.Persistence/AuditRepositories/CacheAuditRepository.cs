using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Providers;

namespace CryptoWatcher.Persistence.AuditRepositories
{
    public class CacheAuditRepository : AuditRepository<Cache>, ICacheRepository
    {
        public CacheAuditRepository(ILogRepository logRepository, IDateTimeProvider dateTimeProvider) : base(logRepository, dateTimeProvider)
        {
        }

        public Task<Cache> GetByKey(string key)
        {
            return Task.FromResult(List.FirstOrDefault(x => x.Key == key));
        }
    }
}
