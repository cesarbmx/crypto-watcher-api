using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Application.Services
{
    public class WatcherService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly ILogger<AddWatcherRequest> _logger;
        private readonly IMapper _mapper;

        public WatcherService(
            MainDbContext mainDbContext,
            IRepository<User> userRepository,
            IRepository<Indicator> indicatorRepository,
            IRepository<Watcher> watcherRepository,
            ILogger<AddWatcherRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _userRepository = userRepository;
            _indicatorRepository = indicatorRepository;
            _watcherRepository = watcherRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<WatcherResponse>> GetAllWatchers(string userId = null, string currencyId = null, string indicatorId = null)
        {
            // Get user
            var user = await _userRepository.GetSingle(userId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all user watchers
            var userWatchers = await _watcherRepository.GetAll(WatcherExpression.WatcherFilter(userId, currencyId, indicatorId));

            // Get default watchers
            var defaultWatchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcherFilter(currencyId, indicatorId));

            // Build with defaults
            userWatchers = WatcherBuilder.BuildWatchersWithDefaults(userWatchers, defaultWatchers);

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(userWatchers);

            // Return
            return response;
        }
        public async Task<WatcherResponse> GetWatcher(Guid watcherId)
        {

            // Get watcher
            var watcher = await _watcherRepository.GetSingle(watcherId);

            // Throw NotFound exception if it does not exist
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

            // Throw NotFound exception if the currency does not exist
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get indicator
            var indicator = await _indicatorRepository.GetSingle(request.IndicatorId);

            // Throw NotFound exception if the currency does not exist
            if (indicator == null) throw new NotFoundException(IndicatorMessage.IndicatorNotFound);

            // Check if it exists
            var watcher = await _watcherRepository.GetSingle(WatcherExpression.Watcher(request.UserId, request.TargetId, request.IndicatorId));

            // Throw NotFound exception if it exists
            if (watcher != null) throw new NotFoundException(WatcherMessage.WatcherExists);

            // Add
            watcher = new Watcher(
                request.UserId,
                request.TargetId,
                request.IndicatorType,
                request.IndicatorId,              
                16,
                request.Buy,
                request.Sell,
                null,
                null,
                request.Enabled);
            _watcherRepository.Add(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

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

            // Throw NotFound exception if it does not exist
            if (watcher == null) throw new NotFoundException(WatcherMessage.WatcherNotFound);

            // Update
            watcher.Update(request.Buy, request.Sell, request.Enabled);
            _watcherRepository.Update(watcher);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
    }
}
