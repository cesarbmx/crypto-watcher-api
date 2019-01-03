using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
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
        [Route("charts")]
        [SwaggerResponse(200, Type = typeof(List<ChartResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(ChartListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
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

