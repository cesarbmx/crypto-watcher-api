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
    public class H_ChartsController : Controller
    {
        private readonly ChartService _chartService;

        public H_ChartsController(ChartService chartService)
        {
            _chartService = chartService;
        }

        /// <summary>
        /// Get all charts
        /// </summary>
        [HttpGet]
        [Route("api/charts")]
        [SwaggerResponse(200, Type = typeof(List<Chart>))]
        [SwaggerOperation(Tags = new[] { "Charts" }, OperationId = "Charts_GetAllCharts")]
        public async Task<IActionResult> GetAllCharts([BindRequired] Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> indicatorIds = null)
        {
            // Reponse
            var response = await _chartService.GetAllCharts(period, currencyIds, indicatorIds);

            // Return
            return Ok(response);
        }
    }
}

