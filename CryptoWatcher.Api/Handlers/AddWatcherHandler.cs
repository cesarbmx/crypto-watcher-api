using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Exceptions;
using CryptoWatcher.Shared.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.Api.Handlers
{
    public class AddWatcherHandler : IRequestHandler<AddWatcherRequest, WatcherResponse>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Indicator> _indicatorRepository;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly ILogger<AddWatcherRequest> _logger;
        private readonly IMapper _mapper;

        public AddWatcherHandler(
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

        public async Task<WatcherResponse> Handle(AddWatcherRequest request, CancellationToken cancellationToken)
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
            var watcher = await _watcherRepository.GetSingle(WatcherExpression.Watcher(request.UserId, request.CurrencyId, request.IndicatorId));

            // Throw NotFound exception if it exists
            if (watcher != null) throw new NotFoundException(WatcherMessage.WatcherExists);

            // Add
            watcher = new Watcher(
                request.UserId, 
                request.CurrencyId, 
                request.IndicatorId,
                16,
                request.BuySell,
                new BuySell(0,0),
                request.Enabled);
            _watcherRepository.Add(watcher);

             // Save
             await _mainDbContext.SaveChangesAsync(cancellationToken);

            // Log into Splunk
            _logger.LogSplunkInformation(request);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
    }
}
