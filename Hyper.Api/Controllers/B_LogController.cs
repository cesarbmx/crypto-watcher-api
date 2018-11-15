using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hyper.Api.ResponseExamples;
using Hyper.Api.Responses;
using Hyper.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Hyper.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class B_LogControllerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly LogService _logService;

        public B_LogControllerController(IMapper mapper, LogService logService)
        {
            _mapper = mapper;
            _logService = logService;
        }

        /// <summary>
        /// Get logs
        /// </summary>
        [HttpGet]
        [Route("logs")]
        [SwaggerResponse(200, Type = typeof(List<LogResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(LogListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Logs" }, OperationId = "Logs_GetLogs")]
        public async Task<IActionResult> GetLogs()
        {
            // Get log
            var log = await _logService.GetLog();

            // Response
            var response = _mapper.Map<List<LogResponse>>(log);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get log
        /// </summary>
        [HttpGet]
        [Route("logs/{id}", Name = "Logs_GetLog")]
        [SwaggerResponse(200, Type = typeof(LogResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(LogResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Logs" }, OperationId = "Logs_GetLog")]
        public async Task<IActionResult> GetLog(string id)
        {
            // Get log
            var log = await _logService.GetLog(id);

            // Response
            var response = _mapper.Map<LogResponse>(log);

            // Return
            return Ok(response);
        }
    }
}

