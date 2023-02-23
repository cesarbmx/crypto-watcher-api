﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Http;
using System.Text.Json;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerOrder(orderPrefix: "A")]
    public class CurrencyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CurrencyService _currencyService;

        public CurrencyController(IHttpClientFactory httpClientFactory, CurrencyService currencyService)
        {
            _httpClientFactory = httpClientFactory;
            _currencyService = currencyService;
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
            // Reponse
            var response = await _currencyService.GetCurrencies();

            var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.GetAsync($"http://crypto-watcher-api2/api/currencies2");


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
    }
}

