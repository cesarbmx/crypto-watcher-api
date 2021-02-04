using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
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
        [SwaggerResponse(200, Type = typeof(ScriptVariableSet))]
        [SwaggerOperation(Tags = new[] { "Script variables" }, OperationId = "ScriptVariableSet_GetScriptVariableSet")]
        public async Task<IActionResult> GetScriptVariableSet([BindRequired] Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> indicatorIds = null)
        {
            // Reponse
            var response = await _scriptVariableService.GetScriptVariableSet(period, currencyIds, indicatorIds);

            // Return
            return Ok(response);
        }
    }
}

