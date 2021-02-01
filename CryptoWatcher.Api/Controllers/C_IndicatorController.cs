using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Application.Services;
using CryptoWatcher.Domain.Types;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    // ReSharper disable once InconsistentNaming
    public class C_IndicatorController : Controller
    {
        private readonly IndicatorService _indicatorService;

        public C_IndicatorController(IndicatorService indicatorService)
        {
            _indicatorService = indicatorService;
        }

        /// <summary>
        /// Get all indicators
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/indicators")]
        [SwaggerResponse(200, Type = typeof(List<Indicator>))]  
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_GetAllIndicators")]
        public async Task<IActionResult> GetAllIndicators(string userId)
        {
            // Reponse
            var response = await _indicatorService.GetAllIndicators(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get indicator
        /// </summary>
        [HttpGet]
        [Route("api/indicators/{indicatorId}", Name = "Indicators_GetIndicator")]
        [SwaggerResponse(200, Type = typeof(Indicator))]
        [SwaggerResponse(404, Type = typeof(Error))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_GetIndicator")]
        public async Task<IActionResult> GetIndicator(string indicatorId)
        {
            // Reponse
            var response = await _indicatorService.GetIndicator(indicatorId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add indicator
        /// </summary>
        [HttpPost]
        [Route("api/indicators")]
        [SwaggerResponse(201, Type = typeof(Indicator))]
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(404, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_AddIndicator")]
        public async Task<IActionResult> AddIndicator([FromBody]AddIndicator request)
        {
            // Reponse
            var response = await _indicatorService.AddIndicator(request);

            // Return
            return CreatedAtRoute("Indicators_GetIndicator", new { response.IndicatorId }, response);
        }

        /// <summary>
        /// Update indicator
        /// </summary>
        [HttpPut]
        [Route("api/indicators/{indicatorId}")]
        [SwaggerResponse(200, Type = typeof(Indicator))]
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_UpdateIndicator")]
        public async Task<IActionResult> UpdateIndicator(string indicatorId, [FromBody]UpdateIndicator request)
        {
            // Reponse
            request.IndicatorId = indicatorId;
            var response = await _indicatorService.UpdateIndicator(request);

            // Return
            return Ok(response);
        }
    }
}

