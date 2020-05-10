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
    public class X_LogController : Controller
    {
        private readonly LogService _logService;

        public X_LogController(LogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Get all logs
        /// </summary>
        [HttpGet]
        [Route("api/logs")]
        [SwaggerResponse(200, Type = typeof(List<LogResponse>))]
        [SwaggerOperation(Tags = new[] { "Logs" }, OperationId = "Logs_GetAllLogs")]
        public async Task<IActionResult> GetAllLogs()
        {
            // Reponse
            var response = await _logService.GetLogs();

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get log
        /// </summary>
        [HttpGet]
        [Route("api/logs/{logId}", Name = "Logs_GetLog")]
        [SwaggerResponse(200, Type = typeof(LogResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerOperation(Tags = new[] { "Logs" }, OperationId = "Logs_GetLog")]
        public async Task<IActionResult> GetLog(Guid logId)
        {
            // Reponse
            var response = await _logService.GetLog(logId);

            // Return
            return Ok(response);
        }
    }
}

