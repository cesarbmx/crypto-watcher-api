using System.Threading.Tasks;
using CesarBmx.Shared.Api.ResponseExamples;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
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
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(ScriptVariableListExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
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

