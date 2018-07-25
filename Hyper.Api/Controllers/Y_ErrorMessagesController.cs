using System.Collections.Generic;
using Hyper.Api.ResponseExamples;
using Hyper.Api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hyper.Domain.Services;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Hyper.Api.Controllers
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
        /// Get all error messages
        /// </summary>
        [HttpGet]
        [Route("error-messages")]
        [SwaggerResponse(200, Type = typeof(Dictionary<string,string[]>))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Error messages" }, OperationId = "ErrorMessages_GetAllErrorMessages")]
        public IActionResult GetAllErrorMessages()
        {
            // Get all error messages
            var errorMessages = _errorMessagesService.GetAllErrorMessages();
            
            // Return
            return Ok(errorMessages);
        }
    }
}

