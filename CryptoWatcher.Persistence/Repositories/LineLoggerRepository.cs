using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;


namespace CryptoWatcher.Persistence.Repositories
{
    public class LineLoggerRepository : LoggerRepository<DataPoint>, ILineRepository
    {
        private readonly LineRepository _lineRepository;

        public LineLoggerRepository(LineRepository repository, Repository<Log> logRepository)
        :base(repository, logRepository)
        {
            _lineRepository = repository;
        }

        public Task<List<DataPoint>> GetCurrentLines()
        {
            return _lineRepository.GetCurrentLines();
        }
    }
}
