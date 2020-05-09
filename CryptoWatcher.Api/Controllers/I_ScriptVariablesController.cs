using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerErrorResponse))]
    [SwaggerResponse(401, Type = typeof(UnauthorizedResponse))]
    [SwaggerResponse(403, Type = typeof(ForbiddenResponse))]
    // ReSharper disable once InconsistentNaming
    public class I_ScriptVariablesController : Controller
    {
        private readonly ScriptVariableService _scriptVariableService;

        public I_ScriptVariablesController(ScriptVariableService scriptVariableService)
        {
            _scriptVariableService = scriptVariableService;
        }

        /// <summary>
        /// Get script variables
        /// </summary>
        [HttpGet]
        [Route("script-variables")]
        [SwaggerResponse(200, Type = typeof(ScriptVariablesResponse))]
        [SwaggerOperation(Tags = new[] { "Script variables" }, OperationId = "ScriptVariables_GetScriptVariables")]
        public async Task<IActionResult> GetScriptVariables()
        {
            // Reponse
            var response = await _scriptVariableService.GetScriptVariables();

            // Return
            return Ok(response);
        }
    }
}

