using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.ModelBuilders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Application.Messages;
using CryptoWatcher.Domain.Models;
using CesarBmx.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class WatcherService
    {
        private readonly DbContext _dbContext;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Line> _lineRepository;
        private readonly ILogger<WatcherService> _logger;
        private readonly IMapper _mapper;

        public WatcherService(
            DbContext dbContext,
            IRepository<User> userRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<Watcher> watcherRepository,
            IRepository<Line> lineRepository,
            ILogger<WatcherService> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _indicatorRepository = indicatorRepository;
            _watcherRepository = watcherRepository;
            _lineRepository = lineRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<WatcherResponse>> GetAllWatchers(string userId = null, string currencyId = null, string indicatorId = null)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Check if it exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all watchers
            var userWatchers = await _watcherRepository.GetAll(WatcherExpression.WatcherFilter(userId, currencyId, indicatorId));

            // Get all default watchers
            var defaultWatchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcher(currencyId, indicatorId));

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
            var watcher = await _watcherRepository.GetSingle(watcherId);

            // Throw NotFound if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
        public async Task<WatcherResponse> AddWatcher(AddWatcher request)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Throw NotFound if the currency does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFound if the currency does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Check if it exists
            var watcher = await _watcherRepository.GetSingle(WatcherExpression.Watcher(request.UserId, request.CurrencyId, request.IndicatorId));

            // Throw ConflictException if it exists
            if (watcher != null) throw new ConflictException(WatcherMessage.WatcherAlreadyExists);

            // Get default watcher
            var defaultWatcher = await _watcherRepository.GetSingle(WatcherExpression.DefaultWatcher(request.CurrencyId, request.IndicatorId));

            // Add
            watcher = new Watcher(
                request.UserId,
                request.CurrencyId,
                request.IndicatorId,
                request.IndicatorType,
                defaultWatcher?.Value,
                request.Buy,
                request.Sell,
                defaultWatcher?.AverageBuy,
                defaultWatcher?.AverageSell,
                request.Enabled,
                DateTime.Now);
            _watcherRepository.Add(watcher);

            // Save
            await _dbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
        public async Task<WatcherResponse> UpdateWatcher(UpdateWatcher request)
        {
            // Get watcher
            var watcher = await _watcherRepository.GetSingle(request.WatcherId);

            // Throw NotFound if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Update watcher
            watcher.Update(request.Buy, request.Sell, request.Enabled);

            // Update
            _watcherRepository.Update(watcher);

            // Save
            await _dbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
        public async Task<List<Watcher>> UpdateWatchers(List<Watcher> defaultWatchers, List<Line> lines)
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get all watchers
            var watchers = await _watcherRepository.GetAll();

            // Sync watchers
            watchers.SyncWatchers(defaultWatchers);

            // Save
            await _dbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateWatchers", new
            {
                watchers.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return
            return watchers;
        }

        public async Task<List<Watcher>> UpdateDefaultWatchers(List<Line> lines)
        {
            // Return if there are no lines
            if (lines.Count == 0) return new List<Watcher>();

            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get newest time
            var newestTime = lines.Max(x => x.Time);

            // Get current lines
            var currentLines = await _lineRepository.GetAll(LineExpression.CurrentLine(newestTime));

            // Build default watchers
            var newDefaultWatchers = WatcherBuilder.BuildDefaultWatchers(currentLines);

            // Get all default watchers
            var defaultWatchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcher());

            // Update 
            _watcherRepository.UpdateCollection(defaultWatchers, newDefaultWatchers);

            // Save
            await _dbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log into Splunk
            _logger.LogSplunkInformation("UpdateDefaultWatchers", new
            {
                newDefaultWatchers.Count,
                ExecutionTime = stopwatch.Elapsed.TotalSeconds
            });

            // Return 
            return defaultWatchers;
        }
    }
}
