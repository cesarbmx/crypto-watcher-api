using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using Hyper.Shared.Providers;

namespace Hyper.Persistence.AuditRepositories
{
    public class CacheAuditRepository : AuditRepository<Cache>, ICacheRepository
    {
        public CacheAuditRepository(ILogRepository logRepository, IDateTimeProvider dateTimeProvider) : base(logRepository, dateTimeProvider)
        {
        }
    }
}
