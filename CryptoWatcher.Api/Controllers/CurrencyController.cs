using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerErrorResponse))]
    [SwaggerResponse(401, Type = typeof(UnauthorizedResponse))]
    [SwaggerResponse(403, Type = typeof(ForbiddenResponse))]
    // ReSharper disable once InconsistentNaming
    public class D_ CurrencysController : Controller
    {
        private readonly  CurrencyService _ currencyService;

        public D_ CurrencysController( CurrencyService  currencyService)
        {
            _ currencyService =  currencyService;
        }

        /// <summary>
        /// Get all  currencys
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/ currencys")]
        [SwaggerResponse(200, Type = typeof(List< CurrencyResponse>))] 
        [SwaggerOperation(Tags = new[] { " Currencys" }, OperationId = " Currencys_GetAll Currencys")]
        public async Task<IActionResult> GetAll Currencys(string userId, string currencyId = null, string indicatorId = null)
        {
            // Reponse
            var response = await _ currencyService.GetAll Currencys(userId, currencyId, indicatorId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get  currency
        /// </summary>
        [HttpGet]
        [Route("api/ currencys/{ currencyId}", Name = " Currencys_Get Currency")]
        [SwaggerResponse(200, Type = typeof( CurrencyResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerOperation(Tags = new[] { " Currencys" }, OperationId = " Currencys_Get Currency")]
        public async Task<IActionResult> Get Currency(string  currencyId)
        {
            // Reponse
            var response = await _ currencyService.Get Currency( currencyId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add  currency
        /// </summary>
        [HttpPost]
        [Route("api/ currencys")]
        [SwaggerResponse(201, Type = typeof( CurrencyResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { " Currencys" }, OperationId = " Currencys_Add Currency")]
        public async Task<IActionResult> Add Currency([FromBody]Add CurrencyRequest request)
        {
            // Reponse
            var response = await _ currencyService.Add Currency(request);

            // Return
            return CreatedAtRoute(" Currencys_Get Currency", new { response. CurrencyId }, response);
        }

        /// <summary>
        /// Update  currency
        /// </summary>
        [HttpPut]
        [Route("api/ currencys/{ currencyId}")]
        [SwaggerResponse(200, Type = typeof( CurrencyResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { " Currencys" }, OperationId = " Currencys_Update Currency")]
        public async Task<IActionResult> Update Currency(string  currencyId, [FromBody]Update CurrencyRequest request)
        {
            // Reponse
            request. CurrencyId =  currencyId;
            var response = await _ currencyService.Update Currency(request);

            // Return
            return Ok(response);
        }
    }
}
