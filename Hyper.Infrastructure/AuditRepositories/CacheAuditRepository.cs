using System;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;

namespace Hyper.Persistence.AuditRepositories
{
    public class CacheAuditRepository : AuditRepository<Cache>, ICacheRepository
    {
        public CacheAuditRepository(ILogRepository logRepository, DateTime dateTime) : base(logRepository, dateTime)
        {
        }
    }
}
