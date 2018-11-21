using System.Collections.Generic;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    [AllowAnonymous]
    public class Y_ErrorMessagesController : Controller
    {
        private readonly ErrorMessagesService _errorMessagesService;

        public Y_ErrorMessagesController(ErrorMessagesService errorMessagesService)
        {
            _errorMessagesService = errorMessagesService;
        }

        /// <summary>
        /// Get error messages
        /// </summary>
        [HttpGet]
        [Route("error-messages")]
        [SwaggerResponse(200, Type = typeof(Dictionary<string,string[]>))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Error messages" }, OperationId = "ErrorMessages_GetErrorMessages")]
        public IActionResult GetErrorMessages()
        {
            // Get error messages
            var errorMessages = _errorMessagesService.GetErrorMessages();
            
            // Return
            return Ok(errorMessages);
        }
    }
}

