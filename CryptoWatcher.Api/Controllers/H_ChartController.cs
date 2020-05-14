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
    [SwaggerResponse(500, Type = typeof(InternalServerErrorResponse))]
    [SwaggerResponse(401, Type = typeof(UnauthorizedResponse))]
    [SwaggerResponse(403, Type = typeof(ForbiddenResponse))]
    // ReSharper disable once InconsistentNaming
    public class H_LineChartsController : Controller
    {
        private readonly LineChartService _lineChartService;

        public H_LineChartsController(LineChartService lineChartService)
        {
            _lineChartService = lineChartService;
        }

        /// <summary>
        /// Get all line charts
        /// </summary>
        [HttpGet]
        [Route("api/line-charts")]
        [SwaggerResponse(200, Type = typeof(List<LineChartResponse>))]
        [SwaggerOperation(Tags = new[] { "Charts" }, OperationId = "LineCharts_GetAllLineCharts")]
        public async Task<IActionResult> GetAllLineCharts(string currencyId = null, IndicatorType? indicatorType = null, string indicatorId = null, string userId = null)
        {
            // Reponse
            var response = await _lineChartService.GetAllLineCharts(currencyId, indicatorType, indicatorId, userId);

            // Return
            return Ok(response);
        }
    }
}

