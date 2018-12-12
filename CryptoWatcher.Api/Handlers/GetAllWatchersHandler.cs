using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Shared.Exceptions;
using MediatR;
using CryptoWatcher.Domain.Builders;

namespace CryptoWatcher.Api.Handlers
{
    public class GetAllWatchersHandler : IRequestHandler<GetAllWatchersRequest, List<WatcherResponse>>
    {
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly CacheService _cacheService;
        private readonly IMapper _mapper;

        public GetAllWatchersHandler(
            IRepository<Watcher> watcherRepository,
            IRepository<User> userRepository,
            IRepository<Indicator> indicatorRepository,
            CacheService cacheService,
            IMapper mapper)
        {
            _watcherRepository = watcherRepository;
            _userRepository = userRepository;
            _indicatorRepository = indicatorRepository;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task<List<WatcherResponse>> Handle(GetAllWatchersRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all watchers
            var watchers = await _watcherRepository.GetAll(WatcherExpression.Filter(request.UserId));

            // Get all currencies
            var currencies = await _cacheService.GetFromCache<Currency>(CacheKey.Currencies);

            // Get all indicators
            var indicators = await _indicatorRepository.GetAll();

            // Build with defaults
            watchers = WatcherBuilder.BuildWatchersWithDefaults(watchers, currencies, indicators);

            // Filter
            if (!string.IsNullOrEmpty(request.IndicatorId))
                watchers = watchers.Where(x => x.IndicatorId == request.IndicatorId).ToList();

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(watchers);

            // Return
            return response;
        }
    }
}
