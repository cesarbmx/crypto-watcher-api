using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Application.Requests;
using CryptoWatcher.Application.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Shared.Exceptions;
using MediatR;
using CryptoWatcher.Domain.Builders;

namespace CryptoWatcher.Application.Handlers
{
    public class GetAllWatchersHandler : IRequestHandler<GetAllWatchersRequest, List<WatcherResponse>>
    {
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetAllWatchersHandler(
            IRepository<Watcher> watcherRepository,
            IRepository<User> userRepository,
            IMapper mapper)
        {
            _watcherRepository = watcherRepository;
            _userRepository = userRepository;
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

            // Get default watchers
            var defaultWatchers = await _watcherRepository.GetAll(WatcherExpression.DefaultWatcher());

            // Build with defaults
            watchers = WatcherBuilder.BuildWatchersWithDefaults(watchers, defaultWatchers);

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
