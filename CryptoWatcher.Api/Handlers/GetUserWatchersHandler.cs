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
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Shared.Exceptions;
using MediatR;
using CryptoWatcher.Domain.Builders;

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
            var user = await _userRepository.GetById(request.UserId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get user watchers
            var userWatchers = await _watcherRepository.Get(WatcherExpression.UserWatcher(request.UserId));

            // Get currencies
            var currencies = await _cacheService.GetFromCache<Currency>();

            // Build with defaults
            userWatchers = userWatchers.BuildWithDefaults(currencies);

            // Filter by indicator type
            if (request.IndicatorType.HasValue)
            {
                userWatchers = userWatchers.Where(x => x.IndicatorType == request.IndicatorType).ToList();
            }

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(userWatchers);

            // Return
            return response;
        }
    }
}
