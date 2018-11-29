using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.RequestExamples;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.ResponseExamples;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using MediatR;
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
        private readonly IMediator _mediator;

        public C_WatchersController(MainDbContext mainDbContext, IMapper mapper, WatcherService watcherService, IMediator mediator)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _watcherService = watcherService;
            _mediator = mediator;
        }

        /// <summary>
        /// Get user watchers
        /// </summary>
        [HttpGet]
        [Route("users/{id}/watchers")]
        [SwaggerResponse(200, Type = typeof(List<WatcherResponse>))]       
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(WatcherListResponseExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetUserWatchers")]
        public async Task<IActionResult> GetUserWatchers(string id, IndicatorType? indicatorType)
        {
            // Reponse
            var response = await _mediator.Send(new GetUserWatchersRequest{Id = id, IndicatorType = indicatorType});

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get watcher
        /// </summary>
        [HttpGet]
        [Route("watchers/{id}", Name = "Watchers_GetWatcher")]
        [SwaggerResponse(200, Type = typeof(WatcherResponse))]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(WatcherResponseExample))]
        [SwaggerResponseExample(404, typeof(NotFoundExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_GetWatcher")]
        public async Task<IActionResult> GetWatcher(string id)
        {
            // Reponse
            var response = await _mediator.Send(new GetWatcherRequest { Id = id });

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
        [SwaggerRequestExample(typeof(AddWatcherRequest), typeof(AddWatcherRequestExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_AddWatcher")]
        public async Task<IActionResult> AddWatcher([FromBody]AddWatcherRequest request)
        {
            // Add watcher
            var watcher = await _watcherService.AddWatcher(request.UserId, request.Indicator, request.CurrencyId, request.Settings);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return CreatedAtRoute("Watchers_GetWatcher", new { response.Id }, response);
        }

        /// <summary>
        /// Update watcher settings
        /// </summary>
        [HttpPut]
        [Route("watchers/{id}/settings")]
        [SwaggerResponse(200, Type = typeof(WatcherResponse))]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        [SwaggerResponse(409, Type = typeof(ErrorResponse))]
        [SwaggerResponse(422, Type = typeof(ValidationResponse))]
        [SwaggerResponse(500, Type = typeof(ErrorResponse))]
        [SwaggerResponseExample(200, typeof(WatcherResponseExample))]
        [SwaggerResponseExample(400, typeof(BadRequestExample))]
        [SwaggerResponseExample(409, typeof(ConflictExample))]
        [SwaggerResponseExample(422, typeof(InvalidRequestExample))]
        [SwaggerResponseExample(500, typeof(InternalServerErrorExample))]
        [SwaggerOperation(Tags = new[] { "Watchers" }, OperationId = "Watchers_UpdateWatcherSettings")]
        public async Task<IActionResult> UpdateWatcherSettings(string id, [FromBody]UpdateWatcherSettingsRequest request)
        {
            // Update watcher settings
            var watcher = await _watcherService.UpdateWatcherSettings(id, request.BuyAt, request.SellAt);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return Ok(response);
        }
    }
}

