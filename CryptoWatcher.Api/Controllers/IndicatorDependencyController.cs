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
    public class IndicatorDependencysController : Controller
    {
        private readonly IndicatorDependencyService _indicatorDependencyService;

        public IndicatorDependencysController(IndicatorDependencyService indicatorDependencyService)
        {
            _indicatorDependencyService = indicatorDependencyService;
        }

        /// <summary>
        /// Get all indicatorDependencys
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/indicatorDependencys")]
        [SwaggerResponse(200, Type = typeof(List<IndicatorDependencyResponse>))] 
        [SwaggerOperation(Tags = new[] { "IndicatorDependencys" }, OperationId = "IndicatorDependencys_GetAllIndicatorDependencys")]
        public async Task<IActionResult> GetAllIndicatorDependencys(string userId, string currencyId = null, string indicatorId = null)
        {
            // Reponse
            var response = await _indicatorDependencyService.GetAllIndicatorDependencys(userId, currencyId, indicatorId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get indicatorDependency
        /// </summary>
        [HttpGet]
        [Route("api/indicatorDependencys/{indicatorDependencyId}", Name = "IndicatorDependencys_GetIndicatorDependency")]
        [SwaggerResponse(200, Type = typeof(IndicatorDependencyResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerOperation(Tags = new[] { "IndicatorDependencys" }, OperationId = "IndicatorDependencys_GetIndicatorDependency")]
        public async Task<IActionResult> GetIndicatorDependency(string indicatorDependencyId)
        {
            // Reponse
            var response = await _indicatorDependencyService.GetIndicatorDependency(indicatorDependencyId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add indicatorDependency
        /// </summary>
        [HttpPost]
        [Route("api/indicatorDependencys")]
        [SwaggerResponse(201, Type = typeof(IndicatorDependencyResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "IndicatorDependencys" }, OperationId = "IndicatorDependencys_AddIndicatorDependency")]
        public async Task<IActionResult> AddIndicatorDependency([FromBody]AddIndicatorDependencyRequest request)
        {
            // Reponse
            var response = await _indicatorDependencyService.AddIndicatorDependency(request);

            // Return
            return CreatedAtRoute("IndicatorDependencys_GetIndicatorDependency", new { response.IndicatorDependencyId }, response);
        }

        /// <summary>
        /// Update indicatorDependency
        /// </summary>
        [HttpPut]
        [Route("api/indicatorDependencys/{indicatorDependencyId}")]
        [SwaggerResponse(200, Type = typeof(IndicatorDependencyResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "IndicatorDependencys" }, OperationId = "IndicatorDependencys_UpdateIndicatorDependency")]
        public async Task<IActionResult> UpdateIndicatorDependency(string indicatorDependencyId, [FromBody]UpdateIndicatorDependencyRequest request)
        {
            // Reponse
            request.IndicatorDependencyId = indicatorDependencyId;
            var response = await _indicatorDependencyService.UpdateIndicatorDependency(request);

            // Return
            return Ok(response);
        }
    }
}
