using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// Get all script variables
        /// </summary>
        [HttpGet]
        [Route("script-variables")]
        [SwaggerResponse(200, Type = typeof(List<Dictionary<string, Dictionary<string, Dictionary<string, decimal>>>>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(ScriptVariableListExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Script variables" }, OperationId = "ScriptVariables_GetAllScriptVariables")]
        public async Task<IActionResult> GetAllScriptVariables()
        {
            // Reponse
            var response = await _scriptVariableService.GetAllScriptVariables();

            // Return
            return Ok(response);
        }
    }
}

