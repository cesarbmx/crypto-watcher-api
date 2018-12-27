using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Lines.Requests;
using CryptoWatcher.Application.Lines.Responses;
using CryptoWatcher.Application.System.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class D_LinesController : Controller
    {
        private readonly IMediator _mediator;

        public D_LinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all lines
        /// </summary>
        [HttpGet]
        [Route("lines")]
        [SwaggerResponse(200, Type = typeof(List<LineResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(LineListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Lines" }, OperationId = "Lines_GetAllLines")]
        public async Task<IActionResult> GetAllLines(string currencyId, string indicatorId)
        {
            // Request
            var request = new GetAllLinesRequest { CurrencyId = currencyId, IndicatorId = indicatorId };

            // Reponse
            var response = await _mediator.Send(request);

            // Return
            return Ok(response);
        }
    }
}

