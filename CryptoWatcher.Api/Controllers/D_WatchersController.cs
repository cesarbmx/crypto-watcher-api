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
    public class D_WatchersController : Controller
    {
        private readonly WatcherService _watcherService;

        public D_WatchersController(WatcherService watcherService)
        {
            _watcherService = watcherService;
        }

        /// <summary>
        /// Get all watchers
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/watchers")]
        [SwaggerResponse(200, Type = typeof(List<WatcherResponse>))] 
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetAllWatchers")]
        public async Task<IActionResult> GetAllWatchers(string userId, string currencyId = null, string indicatorId = null)
        {
            // Reponse
            var response = await _watcherService.GetAllWatchers(userId, currencyId, indicatorId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get watcher
        /// </summary>
        [HttpGet]
        [Route("api/watchers/{watcherId}", Name = "Watchers_GetWatcher")]
        [SwaggerResponse(200, Type = typeof(WatcherResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetWatcher")]
        public async Task<IActionResult> GetWatcher(string watcherId)
        {
            // Reponse
            var response = await _watcherService.GetWatcher(watcherId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Add watcher
        /// </summary>
        [HttpPost]
        [Route("api/watchers")]
        [SwaggerResponse(201, Type = typeof(WatcherResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_AddWatcher")]
        public async Task<IActionResult> AddWatcher([FromBody]AddWatcherRequest request)
        {
            // Reponse
            var response = await _watcherService.AddWatcher(request);

            // Return
            return CreatedAtRoute("Watchers_GetWatcher", new { response.WatcherId }, response);
        }

        /// <summary>
        /// Update watcher
        /// </summary>
        [HttpPut]
        [Route("api/watchers/{watcherId}")]
        [SwaggerResponse(200, Type = typeof(WatcherResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_UpdateWatcher")]
        public async Task<IActionResult> UpdateWatcher(string watcherId, [FromBody]UpdateWatcherRequest request)
        {
            // Reponse
            request.WatcherId = watcherId;
            var response = await _watcherService.UpdateWatcher(request);

            // Return
            return Ok(response);
        }
    }
}

