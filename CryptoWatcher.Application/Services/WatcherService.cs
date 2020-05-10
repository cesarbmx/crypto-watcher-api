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
using CryptoWatcher.Domain.Builders;
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
            var user = await _userRepository.GetSingle(request.UserId);

            // Throw NotFoundException if the currency does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFoundException if the currency does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Check if it exists
            var watcher = await _watcherRepository.GetSingle(WatcherExpression.Watcher(request.UserId, request.TargetId, request.IndicatorId));

            // Throw ConflictException if it exists
            if (watcher != null) throw new ConflictException(WatcherMessage.WatcherAlreadyExists);

            // Time
            var time = DateTime.Now;

            // Get default watcher
            var defaultWatcher = await _watcherRepository.GetSingle(WatcherExpression.DefaultWatcher(request.TargetId, request.IndicatorId));

            // Add
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
                request.Enabled,
                time);
            _watcherRepository.Add(watcher, time);

            // Save
            await _dbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
        public async Task<WatcherResponse> UpdateWatcher(UpdateWatcherRequest request)
        {
            // Get watcher
            var watcher = await _watcherRepository.GetSingle(request.WatcherId);

            // Throw NotFoundException if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Time
            var time = DateTime.Now;

            // Update watcher
            watcher.Update(request.Buy, request.Sell, request.Enabled);

            // Update
            _watcherRepository.Update(watcher, time);

            // Save
            await _dbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
        public async Task UpdateWatchers()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Get all watchers
            var watchers = await _watcherRepository.GetAll();

            // Get all default watchers
            var defaultWatchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcher());

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
        }

        public async Task UpdateDefaultWatchers()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Time
            var time = DateTime.Now;

            // Get al lines
            var allLines = await _lineRepository.GetAll();

            // Return if there are no lines
            if (!allLines.Any()) return;

            // Get newest time
            var newestTime = allLines.Max(x => x.Time);

            // Get current lines
            var currentLines = await _lineRepository.GetAll(LineExpression.CurrentLine(newestTime));

            // Build default watchers
            var newDefaultWatchers = WatcherBuilder.BuildDefaultWatchers(currentLines, time);

            // Get all default watchers
            var defaultWatchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcher());

            // Update 
            _watcherRepository.UpdateCollection(defaultWatchers, newDefaultWatchers, time);

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
        }
    }
}
