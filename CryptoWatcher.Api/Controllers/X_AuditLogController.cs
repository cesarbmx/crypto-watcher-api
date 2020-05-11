using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerErrorResponse))]
    [SwaggerResponse(401, Type = typeof(UnauthorizedResponse))]
    [SwaggerResponse(403, Type = typeof(ForbiddenResponse))]
    // ReSharper disable once InconsistentNaming
    public class X_AuditLogController : Controller
    {
        private readonly AuditLogService _logService;

        public X_AuditLogController(AuditLogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Get all audit logs
        /// </summary>
        [HttpGet]
        [Route("api/audit-logs")]
        [SwaggerResponse(200, Type = typeof(List<AuditLogResponse>))]
        [SwaggerOperation(Tags = new[] { "Audit logs" }, OperationId = "Logs_GetAllAuditLogs")]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            // Reponse
            var response = await _logService.GetAuditLogs();

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get audit log
        /// </summary>
        [HttpGet]
        [Route("api/audit-logs/{logId}", Name = "Logs_GetLog")]
        [SwaggerResponse(200, Type = typeof(AuditLogResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerOperation(Tags = new[] { "Audit logs" }, OperationId = "Logs_GetAuditLog")]
        public async Task<IActionResult> GetAuditLog(Guid logId)
        {
            // Reponse
            var response = await _logService.GetAuditLog(logId);

            // Return
            return Ok(response);
        }
    }
}

