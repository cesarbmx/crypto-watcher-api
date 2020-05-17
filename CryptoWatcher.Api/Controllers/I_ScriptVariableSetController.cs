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
    public class I_ScriptVariableSetController : Controller
    {
        private readonly ScriptVariableSetService _scriptVariableService;

        public I_ScriptVariableSetController(ScriptVariableSetService scriptVariableService)
        {
            _scriptVariableService = scriptVariableService;
        }

        /// <summary>
        /// Get script variables
        /// </summary>
        [HttpGet]
        [Route("api/script-variables")]
        [SwaggerResponse(200, Type = typeof(ScriptVariableSetResponse))]
        [SwaggerOperation(Tags = new[] { "Script variables" }, OperationId = "ScriptVariableSet_GetScriptVariableSet")]
        public async Task<IActionResult> GetScriptVariableSet()
        {
            // Reponse
            var response = await _scriptVariableService.GetScriptVariableSet();

            // Return
            return Ok(response);
        }
    }
}

