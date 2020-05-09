using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Api.ResponseExamples;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
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
        [Route("logs")]
        [SwaggerResponse(200, Type = typeof(List<LogResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(LogListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
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
        [Route("logs/{logId}", Name = "Logs_GetLog")]
        [SwaggerResponse(200, Type = typeof(LogResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(LogResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
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

