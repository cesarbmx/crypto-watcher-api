using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerControllerOrder(orderPrefix: "H")]
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
        public async Task<IActionResult> GetScriptVariables([BindRequired]Period period, List<string> currencyIds, List<string> userIds, List<string> indicatorIds)
        {
            // Reponse
            var response = await _scriptVariableService.GetScriptVariables(period, currencyIds, userIds, indicatorIds);

            // Return
            return Ok(response);
        }
    }
}

