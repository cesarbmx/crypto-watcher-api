using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Charts.Requests;
using CryptoWatcher.Application.Charts.Responses;
using CryptoWatcher.Application.System.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class H_ChartsController : Controller
    {
        private readonly IMediator _mediator;

        public H_ChartsController(IMediator mediator)
        {
            _mediator = mediator;
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
            // Request
            var request = new GetAllChartsRequest { CurrencyId = currencyId, IndicatorId = indicatorId};

            // Reponse
            var response = await _mediator.Send(request);

            // Return
            return Ok(response);
        }
    }
}

