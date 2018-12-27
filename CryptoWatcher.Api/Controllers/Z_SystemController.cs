using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.System.Requests;
using CryptoWatcher.Application.System.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    [AllowAnonymous]
    public class Z_SystemController : Controller
    {
        private readonly IMediator _mediator;

        public Z_SystemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ActionName("ResourceNotFound")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ResourceNotFound()
        {
            var errorResponse = new ErrorResponse(nameof(Message.NotFound), 404, Message.NotFound);
            return NotFound(errorResponse);
        }

        /// <summary>
        /// Get all error messages
        /// </summary>
        [HttpGet]
        [Route("error-messages")]
        [SwaggerResponse(200, Type = typeof(Dictionary<string, string[]>))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Error messages" }, OperationId = "ErrorMessages_GetAllErrorMessages")]
        public IActionResult GetAllErrorMessages()
        {
            // Get error messages
            var errorMessages = ErrorMessageBuilder.BuildErrorMessages();

            // Return
            return Ok(errorMessages);
        }

        /// <summary>
        /// Get version
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
            // Request
            var request = new GetVersionRequest();

            // Reponse
            var response = await _mediator.Send(request);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get health
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
            // Request
            var request = new GetHealthRequest();

            // Reponse
            var response = await _mediator.Send(request);

            // Return
            return Ok(response);
        }
    }
}

