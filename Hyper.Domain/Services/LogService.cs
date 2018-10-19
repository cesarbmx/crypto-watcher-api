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
        public void LogInfo(Event @event)
        {
            // Create log
            var log = new Log(LogLevel.Info, Event.CurrenciesImported);

            // Add Log
            _logRepository.Add(log);
        }
        public void LogError(Event @event)
        {
            // Create log
            var log = new Log(LogLevel.Error, Event.CurrenciesImported);

            // Add Log
            _logRepository.Add(log);
        }
    }
}
