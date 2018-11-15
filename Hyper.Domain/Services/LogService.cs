using System.Collections.Generic;
using System.Threading.Tasks;
using Hyper.Domain.Messages;
using Hyper.Domain.Models;
using Hyper.Domain.Repositories;
using Hyper.Shared.Exceptions;

namespace Hyper.Domain.Services
{
    public class LogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<List<Log>> GetLog()
        {
            // Get Log
            return await _logRepository.GetAll();
        }
        public async Task<Log> GetLog(string id)
        {
            // Get Log by key
            var log = await _logRepository.GetByKey(id);

            // Throw NotFound exception if it does not exist
            if (log == null) throw new NotFoundException(LogMessages.NotFound);

            // Return
            return log;
        }
        public void Log(Log log)
        {
            // Add log
            _logRepository.Add(log);
        }
    }
}
