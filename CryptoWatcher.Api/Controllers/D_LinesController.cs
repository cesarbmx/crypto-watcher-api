using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Shared.ResponseExamples;
using CryptoWatcher.Shared.Responses;
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
            // Reponse
            var response = await _mediator.Send(new GetAllLinesRequest{CurrencyId = currencyId , IndicatorId = indicatorId});

            // Return
            return Ok(response);
        }
    }
}

