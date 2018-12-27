using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Watchers.Requests;
using CryptoWatcher.Application.Watchers.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Shared.Exceptions;
using MediatR;
using CryptoWatcher.Domain.Builders;

namespace CryptoWatcher.Application.Watchers.Handlers
{
    public class GetAllWatchersHandler : IRequestHandler<GetAllWatchersRequest, List<WatcherResponse>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IMapper _mapper;

        public GetAllWatchersHandler(
            IRepository<User> userRepository,
            IRepository<Watcher> watcherRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _watcherRepository = watcherRepository;
            _mapper = mapper;
        }

        public async Task<List<WatcherResponse>> Handle(GetAllWatchersRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetSingle(request.UserId);

            // Check if user exists
            if (user == null) throw new NotFoundException(UserMessage.UserNotFound);

            // Get all user watchers
            var userWatchers = await _watcherRepository.GetAll(WatcherExpression.WatcherFilter(request.UserId, request.CurrencyId, request.IndicatorId));

            // Get default watchers
            var defaultWatchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcherFilter(request.CurrencyId, request.IndicatorId));

            // Build with defaults
            userWatchers = WatcherBuilder.BuildWatchersWithDefaults(userWatchers, defaultWatchers);

            // Response
            var response = _mapper.Map<List<WatcherResponse>>(userWatchers);

            // Return
            return response;
        }
    }
}
