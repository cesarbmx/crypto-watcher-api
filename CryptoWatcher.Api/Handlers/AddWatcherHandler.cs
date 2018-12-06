using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoWatcher.Api.Requests;
using CryptoWatcher.Api.Responses;
using CryptoWatcher.Domain.Expressions;
using CryptoWatcher.Domain.Messages;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
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
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly ILogger<AddWatcherRequest> _logger;
        private readonly IMapper _mapper;

        public AddWatcherHandler(
            MainDbContext mainDbContext,
            IRepository<Watcher> watcherRepository,
            ILogger<AddWatcherRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _watcherRepository = watcherRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<WatcherResponse> Handle(AddWatcherRequest request, CancellationToken cancellationToken)
        {
            // Check if it exists
            var watcher = await _watcherRepository.GetSingle(WatcherExpression.Watcher(request.UserId, request.CurrencyId, request.IndicatorType));

            // Throw NotFound exception if it does not exist
            if (watcher != null) throw new NotFoundException(WatcherMessage.WatcherExists);

            // Add
            watcher = new Watcher(
                request.UserId, 
                request.CurrencyId, 
                request.IndicatorType,
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
