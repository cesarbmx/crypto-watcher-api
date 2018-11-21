using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class B_WatchersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly WatcherService _watcherService;

        public B_WatchersController(IMapper mapper, WatcherService watcherService)
        {
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
            var watcher = await _watcherService.GetWatcher();

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(watcher);

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get user watcher
        /// </summary>
        [HttpGet]
        [Route("users/{userId}/watchers/{watcherId}", Name = "Watchers_GetWatcher")]
        [SwaggerResponse(200, Type = typeof(WatcherResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(WatcherResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetUserWatcher")]
        public async Task<IActionResult> GetWatcher(string userId, string watcherId)
        {
            // Get watcher
            var watcher = await _watcherService.GetWatcher(watcherId);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return Ok(response);
        }
    }
}

