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
        /// Get all user indicators
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/indicators")]
        [SwaggerResponse(200, Type = typeof(List<Indicator>))]  
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_GetAllUserIndicators")]
        public async Task<IActionResult> GetAllUserIndicators(string userId)
        {
            // Reponse
            var response = await _indicatorService.GetAllUserIndicators(userId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get user indicator
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/indicators/{indicatorId}", Name = "Indicators_GetUserIndicator")]
        [SwaggerResponse(200, Type = typeof(Indicator))]
        [SwaggerResponse(404, Type = typeof(Error))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_GetUserIndicator")]
        public async Task<IActionResult> GetUserIndicator(string userId, string indicatorId)
        {
            // Reponse
            var response = await _indicatorService.GetUserIndicator(userId, indicatorId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add user indicator
        /// </summary>
        [HttpPost]
        [Route("api/users/{userId}/indicators")]
        [SwaggerResponse(201, Type = typeof(Indicator))]
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(404, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_AddUserIndicator")]
        public async Task<IActionResult> AddUserIndicator(string userId, [FromBody]AddIndicator request)
        {
            // Request
            request.UserId = userId;

            // Reponse
            var response = await _indicatorService.AddUserIndicator(request);

            // Return
            return CreatedAtRoute("Indicators_GetUserIndicator", new { response.UserId, response.IndicatorId }, response);
        }

        /// <summary>
        /// Update user indicator
        /// </summary>
        [HttpPut]
        [Route("api/users/{userId}/indicators/{indicatorId}")]
        [SwaggerResponse(200, Type = typeof(Indicator))]
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "Indicators" }, OperationId = "Indicators_UpdateUserIndicator")]
        public async Task<IActionResult> UpdateIndicator(string userId, string indicatorId, [FromBody]UpdateIndicator request)
        {
            // Request
            request.UserId = userId;
            request.IndicatorId = indicatorId;

            // Reponse
            var response = await _indicatorService.UpdateUserIndicator(request);

            // Return
            return Ok(response);
        }
    }
}

