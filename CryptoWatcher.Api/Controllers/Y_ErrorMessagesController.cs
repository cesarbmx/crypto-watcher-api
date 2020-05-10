using System.Collections.Generic;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Domain.Builders;
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
    public class Y_ErrorMessagesController : Controller
    {
        /// <summary>
        /// Get all error messages
        /// </summary>
        [HttpGet]
        [Route("api/error-messages")]
        [SwaggerResponse(200, Type = typeof(Dictionary<string,string[]>))]
        [SwaggerOperation(Tags = new[] { "Error messages" }, OperationId = "ErrorMessages_GetAllErrorMessages")]
        public IActionResult GetAllErrorMessages()
        {
            // Get error messages
            var errorMessages = ErrorMessageBuilder.BuildErrorMessages();
            
            // Return
            return Ok(errorMessages);
        }
    }
}

