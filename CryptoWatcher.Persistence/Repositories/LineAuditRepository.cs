using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Providers;


namespace CryptoWatcher.Persistence.Repositories
{
    public class LineAuditRepository : AuditRepository<DataPoint>, ILineRepository
    {
        public LineAuditRepository(Repository<Log> logRepository, DateTimeProvider dateTimeProvider)
            : base(logRepository, dateTimeProvider)
        {
        }

        public Task<List<DataPoint>> GetCurrentLines()
        {
            var row = List.OrderByDescending(t => t.Time).FirstOrDefault();
            if (row == null) return Task.FromResult(new List<DataPoint>());
            var maxDate = row.Time;
            return Task.FromResult(List.Where(x => x.Time == maxDate).ToList());
        }
    }
}
