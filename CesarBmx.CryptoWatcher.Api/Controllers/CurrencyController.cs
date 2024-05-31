using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MassTransit;
using CesarBmx.Shared.Messaging.Ordering.Commands;
using Microsoft.Extensions.Caching.Hybrid;
using Azure.Core;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerOrder(orderPrefix: "A")]
    public class CurrencyController : Controller
    {
        private readonly CurrencyService _currencyService;
        private readonly HybridCache _hybridCache;

        public CurrencyController(CurrencyService currencyService, HybridCache hybridCache)
        {
            _currencyService = currencyService;
            _hybridCache = hybridCache;
        }

        /// <summary>
        /// Get currencies
        /// </summary>
        [HttpGet]
        [Route("api/currencies")]
        [SwaggerResponse(200, Type = typeof(List<Currency>))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            // Cached reponse
            var response = await _hybridCache.GetOrCreateAsync($"GetCurrencies", async cancel => await _currencyService.GetCurrencies());

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get currency
        /// </summary>
        [HttpGet]
        [Route("api/currencies/{currencyId}", Name = "Currencies_GetCurrency")]
        [SwaggerResponse(200, Type = typeof(Currency))]
        [SwaggerResponse(404, Type = typeof(NotFound))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrency")]
        public async Task<IActionResult> GetCurrency(string currencyId)
        {
            // Reponse
            var response = await _currencyService.GetCurrency(currencyId);

            // Return
            return Ok(response);

        }
        /// <summary>
        /// Get currency
        /// </summary>
        [HttpGet]
        [Route("api/currencies/{currencyId:guid}", Name = "Currencies_GetCurrencyByGuid")]
        [SwaggerResponse(200, Type = typeof(Currency))]
        [SwaggerResponse(404, Type = typeof(NotFound))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrencyByGuid")]
        public IActionResult GetCurrencyByGuid(Guid currencyId)
        {
            // Return
            return Ok(currencyId);
        }
    }

}

