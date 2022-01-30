using System.Threading.Tasks;
using CesarBmx.CryptoWatcher.Application.Queries;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerControllerOrder(orderPrefix: "I")]
    public class ScriptVariablesController : Controller
    {
        private readonly ScriptVariablesService _scriptVariableService;

        public ScriptVariablesController(ScriptVariablesService scriptVariableService)
        {
            _scriptVariableService = scriptVariableService;
        }

        /// <summary>
        /// Get script variables
        /// </summary>
        [HttpGet]
        [Route("api/script-variables")]
        [SwaggerResponse(200, Type = typeof(ScriptVariables))]
        [SwaggerOperation(Tags = new[] { "Script variables" }, OperationId = "ScriptVariables_GetScriptVariables")]
        public async Task<IActionResult> GetScriptVariables(GetScriptVariables query)
        {
            // Reponse
            var response = await _scriptVariableService.GetScriptVariables(query);

            // Return
            return Ok(response);
        }
    }
}

