using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Types;
using Microsoft.AspNetCore.Mvc;
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
        /// Get all line charts
        /// </summary>
        [HttpGet]
        [Route("api/line-charts")]
        [SwaggerResponse(200, Type = typeof(List<Chart>))]
        [SwaggerOperation(Tags = new[] { "Charts" }, OperationId = "Charts_GetAllCharts")]
        public async Task<IActionResult> GetAllCharts(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            // Reponse
            var response = await _chartService.GetAllCharts(currencyId, indicatorType, indicatorId, userId);

            // Return
            return Ok(response);
        }
    }
}

