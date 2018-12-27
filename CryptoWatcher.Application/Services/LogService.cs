using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;

namespace CryptoWatcher.Application.Services
{
    public class LogService
    {
        private readonly IRepository<Log> _logRepository;
        private readonly IMapper _mapper;

        public LogService(IRepository<Log> logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<List<LogResponse>> GetLogs()
        {
            // Get logs
            var logs = await _logRepository.GetAll();

            // Response
            var response = _mapper.Map<List<LogResponse>>(logs);

            // Return
            return response;
        }
        public async Task<LogResponse> GetLog(Guid logId)
        {
            // Get log
            var log = await _logRepository.GetSingle(logId);

            // Throw NotFound exception if the currency does not exist
            if (log == null) throw new NotFoundException(LogMessage.LogNotFound);

            // Response
            var response = _mapper.Map<LogResponse>(log);

            // Return
            return response;
        }
    }
}
