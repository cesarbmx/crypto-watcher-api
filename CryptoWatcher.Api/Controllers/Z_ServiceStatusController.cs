using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    [AllowAnonymous]
    public class Z_ServiceStatusController : Controller
    {
        private readonly IMapper _mapper;
        private readonly StatusService _statusService;
        private readonly IHostingEnvironment _env;

        public Z_ServiceStatusController(IMapper mapper, StatusService statusService, IHostingEnvironment env)
        {
            _mapper = mapper;
            _statusService = statusService;
            _env = env;
        }

        [HttpGet]
        [ActionName("ResourceNotFound")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ResourceNotFound()
        {
            var errorResponse = new ErrorResponse(nameof(Messages.NotFound), 404, Messages.NotFound);
            return NotFound(errorResponse);
        }

        /// <summary>
        /// Get service version
        /// </summary>
        [HttpGet]
        [Route("version")]
        [SwaggerResponse(200, Type = typeof(VersionResponse))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(VersionResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Service status" }, OperationId = "ServiceStatus_GetVersion")]
        public async Task<IActionResult> GetVersion()
        {
            // Get status
            var version = await _statusService.GetVersion(_env.EnvironmentName);

            // Response
            var response = _mapper.Map<VersionResponse>(version);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get service health
        /// </summary>
        [HttpGet]
        [Route("health")]
        [SwaggerResponse(200, Type = typeof(HealthResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(HealthResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Service status" }, OperationId = "ServiceStatus_GetHealth")]
        public async Task<IActionResult> GetHealth()
        {
            // Get health
            var health = await _statusService.GetHealth();

            // Response
            var response = Mapper.Map<HealthResponse>(health);

            // Return
            return Ok(response);
        }
    }
}

