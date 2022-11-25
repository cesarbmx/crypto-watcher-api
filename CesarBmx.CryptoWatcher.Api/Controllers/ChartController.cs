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
    [SwaggerOrder(orderPrefix: "I")]
    public class ChartsController : Controller
    {
        private readonly ChartService _chartService;
        private readonly AppSettings _appSettings;

        public ChartsController(ChartService chartService, AppSettings appSettings)
        {
            _chartService = chartService;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Get charts
        /// </summary>
        [HttpGet]
        [Route("api/charts")]
        [SwaggerResponse(200, Type = typeof(List<Chart>))]
        [SwaggerOperation(Tags = new[] { "Charts" }, OperationId = "Charts_GetCharts")]
        public async Task<IActionResult> GetCharts([BindRequired] Period period = Period.ONE_MINUTE, List<string> currencyIds = null, List<string> userIds = null, List<string> indicatorIds = null)
        {
            // Reponse
            var response = await _chartService.GetCharts(_appSettings.LineRetention, period, currencyIds, userIds, indicatorIds);

            // Return
            return Ok(response);
        }
    }
}

