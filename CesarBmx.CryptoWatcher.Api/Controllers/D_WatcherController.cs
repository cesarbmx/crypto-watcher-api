using System.Collections.Generic;
using System.Threading.Tasks;
using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Requests;
using CesarBmx.CryptoWatcher.Application.Resources;
using CesarBmx.CryptoWatcher.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    // ReSharper disable once InconsistentNaming
    public class D_WatcherController : Controller
    {
        private readonly WatcherService _watcherService;

        public D_WatcherController(WatcherService watcherService)
        {
            _watcherService = watcherService;
        }

        /// <summary>
        /// Get user watchers
        /// </summary>
        [HttpGet]
        [Route("api/users/{userId}/watchers")]
        [SwaggerResponse(200, Type = typeof(List<Watcher>))] 
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetUserWatchers")]
        public async Task<IActionResult> GetUserWatchers(string userId, string currencyId = null, string indicatorId = null)
        {
            // Reponse
            var response = await _watcherService.GetUserWatchers(userId, currencyId, indicatorId);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get watcher
        /// </summary>
        [HttpGet]
        [Route("api/watchers/{watcherId}", Name = "Watchers_GetWatcher")]
        [SwaggerResponse(200, Type = typeof(Watcher))]
        [SwaggerResponse(404, Type = typeof(Error))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetWatcher")]
        public async Task<IActionResult> GetWatcher(int watcherId)
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
        [SwaggerResponse(201, Type = typeof(Watcher))]
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(404, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
        [SwaggerResponse(422, Type = typeof(ValidationFailed))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_AddWatcher")]
        public async Task<IActionResult> AddWatcher([FromBody]AddWatcher request)
        {
            // Reponse
            var response = await _watcherService.AddWatcher(request);

            // Return
            return CreatedAtRoute("Watchers_GetWatcher",  new {response.WatcherId}, response);
        }

        /// <summary>
        /// Set watcher
        /// </summary>
        [HttpPut]
        [Route("api/watchers/{watcherId}")]
        [SwaggerResponse(200, Type = typeof(Watcher))]
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
        [SwaggerResponse(422, Type = typeof(ValidationFailed))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_SetWatcher")]
        public async Task<IActionResult> SetWatcher(int watcherId, [FromBody]SetWatcher request)
        {
            // Request
            request.WatcherId = watcherId;

            // Reponse
            var response = await _watcherService.SetWatcher(request);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Enable/Disable watcher
        /// </summary>
        [HttpPut]
        [Route("api/watchers/{watcherId}/enabled")]
        [SwaggerResponse(200, Type = typeof(Watcher))]
        [SwaggerResponse(400, Type = typeof(Error))]
        [SwaggerResponse(409, Type = typeof(Error))]
        [SwaggerResponse(422, Type = typeof(ValidationFailed))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_EnableWatcher")]
        public async Task<IActionResult> EnableWatcher(int watcherId, [FromBody] EnableWatcher request)
        {
            // Request
            request.WatcherId = watcherId;

            // Reponse
            var response = await _watcherService.EnableWatcher(request);

            // Return
            return Ok(response);
        }
    }
}

