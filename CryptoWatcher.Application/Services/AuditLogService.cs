using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Application.Messages;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.Shared.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;

namespace CryptoWatcher.Application.Services
{
    public class AuditLogService
    {
        private readonly IRepository<AuditLog> _logRepository;
        private readonly IMapper _mapper;

        public AuditLogService(IRepository<AuditLog> logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<List<AuditLogResponse>> GetAuditLogs()
        {
            // Get all audit logs
            var logs = await _logRepository.GetAll();

            // Response
            var response = _mapper.Map<List<AuditLogResponse>>(logs);

            // Return
            return response;
        }
        public async Task<AuditLogResponse> GetAuditLog(Guid logId)
        {
            // Get audit log
            var auditLog = await _logRepository.GetSingle(logId);

            // Throw NotFound if the currency does not exist
            if (auditLog == null) throw new NotFoundException(AuditLogMessage.LogNotFound);

            // Response
            var response = _mapper.Map<AuditLogResponse>(auditLog);

            // Return
            return response;
        }
    }
}
