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
using CesarBmx.CryptoWatcher.Application.Settings;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerOrder(orderPrefix: "H")]
    public class ScriptVariablesController : Controller
    {
        private readonly ScriptVariablesService _scriptVariableService;
        private readonly AppSettings _appSettings;

        public ScriptVariablesController(ScriptVariablesService scriptVariableService, AppSettings appSettings)
        {
            _scriptVariableService = scriptVariableService;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Get script variables
        /// </summary>
        [HttpGet]
        [Route("api/script-variables")]
        [SwaggerResponse(200, Type = typeof(ScriptVariablesResponse))]
        [SwaggerOperation(Tags = new[] { "Script variables" }, OperationId = "ScriptVariables_GetScriptVariables")]
        public async Task<IActionResult> GetScriptVariables([BindRequired]Period period, List<string> currencyIds, List<string> userIds, List<string> indicatorIds)
        {
            // Reponse
            var response = await _scriptVariableService.GetScriptVariables(_appSettings.LineRetention, period, currencyIds, userIds, indicatorIds);

            // Return
            return Ok(response);
        }
    }
}

