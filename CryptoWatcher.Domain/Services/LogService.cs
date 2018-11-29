using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Domain.Services
{
    public class LogService
    {
        private readonly IRepository<Log> _logRepository;

        public LogService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<List<Log>> GetLog()
        {
            // Get log
            return await _logRepository.GetAll();
        }
        public async Task<Log> GetLog(string id)
        {
            // Get log
            var log = await _logRepository.GetById(id);

            // Throw NotFound exception if it does not exist
            if (log == null) throw new NotFoundException(LogMessage.LogNotFound);

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
