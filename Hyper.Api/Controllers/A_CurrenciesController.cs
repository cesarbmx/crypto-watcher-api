using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class A_CurrenciesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly CurrencyService _currencyService;

        public A_CurrenciesController(IMapper mapper, CurrencyService currencyService)
        {
            _mapper = mapper;
            _currencyService = currencyService;
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
            // Get all currencies
            var currencies = await _currencyService.GetAllCurrencies();

            // Response
            var response = _mapper.Map<List<CurrencyResponse>>(currencies);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get currency
        /// </summary>
        [HttpGet]
        [Route("currencies/{id}", Name = "Taxes_GetCurrency")]
        [SwaggerResponse(200, Type = typeof(CurrencyResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(CurrencyResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrency")]
        public async Task<IActionResult> GetCurrency(string id)
        {
            // Get currency
            var currency = await _currencyService.GetCurrency(id);

            // Response
            var response = _mapper.Map<CurrencyResponse>(currency);

            // Return
            return Ok(response);
        }
    }
}

