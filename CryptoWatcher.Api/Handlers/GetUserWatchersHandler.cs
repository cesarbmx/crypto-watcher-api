using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Shared.Exceptions;
using MediatR;

namespace CryptoWatcher.Api.Handlers
{
    public class GetUserWatchersHandler : IRequestHandler<GetUserWatchersRequest, List<WatcherResponse>>
    {
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<User> _userRepository;
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;

        public GetUserWatchersHandler(
            IRepository<Watcher> watcherRepository,
            IRepository<User> userRepository,
            CacheService cacheService,
            IMapper mapper)
        {
            _watcherRepository = watcherRepository;
            _userRepository = userRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<List<WatcherResponse>> Handle(GetUserWatchersRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetById(request.Id);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user watchers
            var userWatchers = await _watcherRepository.Get(WatcherExpression.UserWatcher(request.Id));

            // Get currencies
            var currencies = await _cacheService.GetFromCache<Currency>();

            // For each currency
            var watchers = new List<Watcher>();
            foreach (var currency in currencies)
            {
                // Price change watcher
                if (!request.IndicatorType.HasValue || request.IndicatorType == IndicatorType.PriceChange)
                {
                    // Get matching watcher
                    var priceChangeWatcher = userWatchers.FirstOrDefault(x =>
                        x.IndicatorType == IndicatorType.PriceChange &&
                        x.Id == currency.Id);

                    // If the watcher does not exist, we add the default one
                    if (priceChangeWatcher == null)
                    {
                        priceChangeWatcher = new Watcher(
                            "master",
                            currency.Id,
                            IndicatorType.PriceChange,
                            IndicatorBuilder.BuildValue(currency, IndicatorType.PriceChange, currencies),
                            new WatcherSettings(5, 5),
                            new WatcherSettings(0, 0),
                            false);
                    }

                    // Add
                    watchers.Add(priceChangeWatcher);
                }

                // Price change watcher
                if (!request.IndicatorType.HasValue || request.IndicatorType == IndicatorType.Hype)
                {
                    // Get matching watcher
                    var hypeWatcher = userWatchers.FirstOrDefault(x =>
                        x.IndicatorType == IndicatorType.Hype &&
                        x.Id == currency.Id);

                    // If the watcher does not exist, we add the default one
                    if (hypeWatcher == null)
                    {
                        hypeWatcher = new Watcher(
                            "master",
                            currency.Id,
                            IndicatorType.PriceChange,
                            IndicatorBuilder.BuildValue(currency, IndicatorType.PriceChange, currencies),
                            new WatcherSettings(5, 5),
                            new WatcherSettings(0, 0),
                            false);
                    }

                    // Add
                    watchers.Add(hypeWatcher);
                }
            }

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(watchers);

            // Return
            return response;
        }
    }
}
