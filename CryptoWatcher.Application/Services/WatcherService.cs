using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class WatcherService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<WatcherService> _logger;
        private readonly IMapper _mapper;

        public WatcherService(
            MainDbContext mainDbContext,
            ILogger<WatcherService> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<WatcherResponse>> GetAllWatchers(string userId = null, string currencyId = null, string indicatorId = null)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all watchers
            var userWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.WatcherFilter(userId, currencyId, indicatorId)).ToListAsync();

            // Get all default watchers
            var defaultWatchers = await _mainDbContext.Watchers.Where(WatcherExpression.DefaultWatcher(currencyId, indicatorId)).ToListAsync();

            // Build with defaults
            userWatchers = WatcherBuilder.BuildWatchersWithDefaults(userWatchers, defaultWatchers);

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(userWatchers);

            // Return
            return response;
        }
        public async Task<WatcherResponse> GetWatcher(string watcherId)
        {
            // Get watcher
            var watcher = await _mainDbContext.Watchers.FindAsync(watcherId);

            // Throw NotFoundException if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
        public async Task<WatcherResponse> AddWatcher(AddWatcherRequest request)
        {
            // Get user
            var user = await _mainDbContext.Users.FindAsync(request.UserId);

            // Throw NotFoundException if the currency does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicator
            var indicator = await _mainDbContext.Indicators.FindAsync(request.IndicatorId);

            // Throw NotFoundException if the currency does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Check if it exists
            var watcher = await _mainDbContext.Watchers.SingleOrDefaultAsync(WatcherExpression.Watcher(request.UserId, request.TargetId, request.IndicatorId));

            // Throw ConflictException if it exists
            if (watcher != null) throw new ConflictException(WatcherMessage.WatcherAlreadyExists);

            // Get default watcher
           var defaultWatcher = await _mainDbContext.Watchers.SingleOrDefaultAsync(WatcherExpression.DefaultWatcher(request.TargetId, request.IndicatorId));

            // Create
            watcher = new Watcher(
                request.UserId,
                request.TargetId,
                request.IndicatorId,
                request.IndicatorType,
                defaultWatcher?.Value,
                request.Buy,
                request.Sell,
                defaultWatcher?.AverageBuy,
                defaultWatcher?.AverageSell,
                request.Enabled);

            // Add
            _mainDbContext.Watchers.Add(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkRequest(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
        public async Task<WatcherResponse> UpdateWatcher(UpdateWatcherRequest request)
        {
            // Get watcher
            var watcher = await _mainDbContext.Watchers.FindAsync(request.WatcherId);

            // Throw NotFoundException if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Update watcer
            watcher.Update(request.Buy, request.Sell, request.Enabled);

            // Update
            _mainDbContext.Watchers.Update(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkRequest(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
    }
}
