using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerErrorResponse))]
    [SwaggerResponse(401, Type = typeof(UnauthorizedResponse))]
    [SwaggerResponse(403, Type = typeof(ForbiddenResponse))]
    // ReSharper disable once InconsistentNaming
    [AllowAnonymous]
    public class Z_ServiceStatusController : Controller
    {
        private readonly StatusService _statusService;

        public Z_ServiceStatusController(StatusService statusService)
        {
            _statusService = statusService;
        }

        /// <summary>
        /// Get version
        /// </summary>
        [HttpGet]
        [Route("version")]
        [SwaggerResponse(200, Type = typeof(VersionResponse))]
        [SwaggerOperation(Tags = new[] { "Service status" }, OperationId = "ServiceStatus_GetVersion")]
        public async Task<IActionResult> GetVersion()
        {
            // Reponse
            var response = await _statusService.GetVersion();
                
            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get health
        /// </summary>
        [HttpGet]
        [Route("health")]
        [SwaggerResponse(200, Type = typeof(HealthResponse))]
        [SwaggerOperation(Tags = new[] { "Service status" }, OperationId = "ServiceStatus_GetHealth")]
        public async Task<IActionResult> GetHealth()
        {
            // Reponse
            var response = await _statusService.GetHealth();

            // Return
            return Ok(response);
        }
    }
}

