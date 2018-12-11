using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class X_LogController : Controller
    {
        private readonly IMediator _mediator;

        public X_LogController(IMediator mediator)
        {
            _mediator = mediator;
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
            var response = await _mediator.Send(new GetLogsRequest());

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
        public async Task<IActionResult> GetLog(string logId)
        {
            // Reponse
            var response = await _mediator.Send(new GetLogRequest() { LogId = logId });

            // Return
            return Ok(response);
        }
    }
}

