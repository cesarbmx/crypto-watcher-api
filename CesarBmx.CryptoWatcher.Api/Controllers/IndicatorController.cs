using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CesarBmx.CryptoWatcher.Application.ConflictReasons;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerControllerOrder(orderPrefix: "C")]
    public class IndicatorController : Controller
    {
        private readonly IndicatorService _indicatorService;

        public IndicatorController(IndicatorService indicatorService)
        {
            _indicatorService = indicatorService;
        }

        /// <summary>
        /// Get user indicators
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/indicators")]
        [SwaggerResponse(200, Type = typeof(List<Indicator>))]  
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_GetUserIndicators")]
        public async Task<IActionResult> GetUserIndicators(string userId)
        {
            // Reponse
            var response = await _indicatorService.GetUserIndicators(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get indicator
        /// </summary>
        [HttpGet]
        [Route("api/indicators/{indicatorId}", Name = "Indicators_GetIndicator")]
        [SwaggerResponse(200, Type = typeof(Indicator))]
        [SwaggerResponse(404, Type = typeof(NotFound))]
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
        [SwaggerResponse(400, Type = typeof(BadRequest))]
        [SwaggerResponse(404, Type = typeof(NotFound))]
        [SwaggerResponse(409, Type = typeof(Conflict<AddIndicatorConflictReason>))]
        [SwaggerResponse(422, Type = typeof(ValidationFailed))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_AddIndicator")]
        public async Task<IActionResult> AddIndicator([FromBody]AddIndicator request)
        {
            // Reponse
            var response = await _indicatorService.AddIndicator(request);

            // Return
            return CreatedAtRoute("Indicators_GetIndicator", new {response.IndicatorId} , response);
        }

        /// <summary>
        /// Update indicator
        /// </summary>
        [HttpPut]
        [Route("api/indicators/{indicatorId}")]
        [SwaggerResponse(200, Type = typeof(Indicator))]
        [SwaggerResponse(400, Type = typeof(BadRequest))]
        [SwaggerResponse(422, Type = typeof(ValidationFailed))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_UpdateIndicator")]
        public async Task<IActionResult> UpdateIndicator(string indicatorId, [FromBody]UpdateIndicator request)
        {
            // Request
            request.UserId = "cesarbmx";
            request.IndicatorId = indicatorId;

            // Reponse
            var response = await _indicatorService.UpdateIndicator(request);

            // Return
            return Ok(response);
        }
    }
}

