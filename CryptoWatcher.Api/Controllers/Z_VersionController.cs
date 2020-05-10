using System.Reflection;
using CesarBmx.Shared.Application.ResponseBuilders;
using CesarBmx.Shared.Application.Responses;
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
    public class Z_VersionController : Controller
    {
        /// <summary>
        /// Get version
        /// </summary>
        [HttpGet]
        [Route("version")]
        [SwaggerResponse(200, Type = typeof(VersionResponse))]
        [SwaggerOperation(Tags = new[] { "Version" }, OperationId = "Version_GetVersion")]
        public IActionResult GetVersion()
        {
            // Response
            var response = new VersionResponse();

            // Build
            response.Build(Assembly.GetExecutingAssembly());
                
            // Return
            return Ok(response);
        }
    }
}

