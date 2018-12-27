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
        public async Task<IActionResult> GetAllCharts(string currencyId, string indicatorId)
        {
            // Reponse
            var response = await _chartService.GetAllCharts(currencyId, indicatorId);

            // Return
            return Ok(response);
        }
    }
}

