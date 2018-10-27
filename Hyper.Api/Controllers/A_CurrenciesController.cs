using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hyper.Api.ResponseExamples;
using Hyper.Api.Responses;
using Hyper.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Hyper.Api.Controllers
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
        public async Task<IActionResult> GetVersion()
        {
            // Get all currencies
            var currencies = await _currencyService.GetAllCurrencies();

            // Response
            var response = _mapper.Map<List<CurrencyResponse>>(currencies);

            // Return
            return Ok(response);
        }
    }
}

