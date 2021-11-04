using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    // ReSharper disable once InconsistentNaming
    public class A_CurrencyController : Controller
    {
        private readonly CurrencyService _currencyService;

        public A_CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        /// <summary>
        /// Get all currencies
        /// </summary>
        [HttpGet]
        [Route("api/currencies")]
        [SwaggerResponse(200, Type = typeof(List<Currency>))]  
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            // Reponse
            var response = await _currencyService.GetAllCurrencies();

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get currency
        /// </summary>
        [HttpGet]
        [Route("api/currencies/{currencyId}", Name = "Currencies_GetCurrency")]
        [SwaggerResponse(200, Type = typeof(Currency))]
        [SwaggerResponse(404, Type = typeof(Error))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrency")]
        public async Task<IActionResult> GetCurrency(string currencyId)
        {
            // Reponse
            var response = await _currencyService.GetCurrency(currencyId);

            // Return
            return Ok(response);
        }
    }
}

