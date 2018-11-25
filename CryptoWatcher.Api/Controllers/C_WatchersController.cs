using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
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
    public class C_WatchersController : Controller
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly WatcherService _watcherService;

        public C_WatchersController(MainDbContext mainDbContext, IMapper mapper, WatcherService watcherService)
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
        public async Task<IActionResult> GetUserWatchers(string userId, WatcherType? watcherType = null)
        {
            // Get watchers
            List<Watcher> watchers;
            if (watcherType.HasValue)
            {
                watchers = await _watcherService.GetWatchers(userId, watcherType.Value);
            }
            else
            {
                watchers = await _watcherService.GetWatchers(userId);
            }

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(watchers);

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
        /// Add watcher
        /// </summary>
        [HttpPost]
        [Route("watchers")]
        [SwaggerResponse(201, Type = typeof(WatcherResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(201, typeof(WatcherResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(409, typeof(ConflictExample))]
        [SwaggerResponseExample(422, typeof(InvalidRequestExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_AddWatcher")]
        public async Task<IActionResult> AddWatcher([FromBody]AddWatcherRequest request)
        {
            // Add watcher
            var watcher = await _watcherService.AddWatcher(request.UserId, request.WatcherType, request.CurrencyId);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return CreatedAtRoute("Watchers_GetWatcher", new { response.WatcherId }, response);
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
        public async Task<IActionResult> UpdateWatcherSettings(string watcherId, [FromBody]UpdateWatcherSettingsRequest request)
        {
            // Update watcher settings
            var watcher = await _watcherService.UpdateWatcherSettings(watcherId, request.BuyAt, request.SellAt);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return Ok(response);
        }
    }
}

