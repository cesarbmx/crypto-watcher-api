using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class A_CurrenciesController : Controller
    {
        private readonly IMediator _mediator;

        public A_CurrenciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all currencies
        /// </summary>
        [HttpGet]
        [Route("currencies")]
        [SwaggerResponse(200, Type = typeof(List<CurrencyResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(CurrencyListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            // Reponse
            var response = await _mediator.Send(new GetAllCurrenciesRequest());

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get currency
        /// </summary>
        [HttpGet]
        [Route("currencies/{currencyId}", Name = "Currencies_GetCurrency")]
        [SwaggerResponse(200, Type = typeof(CurrencyResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(CurrencyResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrency")]
        public async Task<IActionResult> GetCurrency(string currencyId)
        {
            // Reponse
            var response = await _mediator.Send(new GetCurrencyRequest { CurrencyId = currencyId });

            // Return
            return Ok(response);
        }
    }
}

