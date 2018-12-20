using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Models;
using CryptoWatcher.Shared.Repositories;


namespace CryptoWatcher.Persistence.Repositories
{
    public class LineLoggerRepository : LoggerRepository<Line>, ILineRepository
    {
        private readonly LineRepository _lineRepository;

        public LineLoggerRepository(LineRepository repository, Repository<Log> logRepository)
        :base(repository, logRepository)
        {
            _lineRepository = repository;
        }

        public Task<List<Line>> GetCurrentLines()
        {
            return _lineRepository.GetCurrentLines();
        }
    }
}
