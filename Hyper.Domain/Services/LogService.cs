using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;

namespace Hyper.Domain.Services
{
    public class LogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<Log>> GetLog()
        {
            // Get Log
            return await _logRepository.GetLog();
        }
        public void Log(Log log)
        {
            // Add log
            _logRepository.Add(log);
        }
    }
}
