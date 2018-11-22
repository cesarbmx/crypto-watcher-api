using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class B_WatchersController : Controller
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly WatcherService _watcherService;

        public B_WatchersController(MainDbContext mainDbContext, IMapper mapper, WatcherService watcherService)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _watcherService = watcherService;
        }

        /// <summary>
        /// Get user watchers
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/watchers")]
        [SwaggerResponse(200, Type = typeof(List<WatcherResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(WatcherListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetUserWatchers")]
        public async Task<IActionResult> GetWatchers(string userId)
        {
            // Get watcher
            var watcher = await _watcherService.GetUserWatchers(userId);

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(watcher);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get watcher
        /// </summary>
        [HttpGet]
        [Route("watchers/{watcherId}", Name = "Watchers_GetWatcher")]
        [SwaggerResponse(200, Type = typeof(WatcherResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(WatcherResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetWatcher")]
        public async Task<IActionResult> GetWatcher(string watcherId)
        {
            // Get watcher
            var watcher = await _watcherService.GetWatcher(watcherId);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Update watcher settings
        /// </summary>
        [HttpPut]
        [Route("watchers/{watcherId}/settings")]
        [SwaggerResponse(200, Type = typeof(WatcherResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(WatcherResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(409, typeof(ConflictExample))]
        [SwaggerResponseExample(422, typeof(InvalidRequestExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_UpdateWatcherSettings")]
        public async Task<IActionResult> UpdateWatcherSettings(string watcherId, [FromBody]WatcherSettings settings)
        {
            // Update watcher settings
            var watcherSettings = await _watcherService.UpdateWatcherSettings(watcherId, settings);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = watcherSettings;

            // Return
            return Ok(response);
        }
    }
}

