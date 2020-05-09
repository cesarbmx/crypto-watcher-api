using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Types;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
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
        [Route("line-charts")]
        [SwaggerResponse(200, Type = typeof(List<LineChartResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(LineChartListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
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

