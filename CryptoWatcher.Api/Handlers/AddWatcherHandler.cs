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
        private readonly IRepository<User> _userRepository;
        private readonly CacheService _cacheService;
        private readonly ILogger<AddWatcherRequest> _logger;
        private readonly IMapper _mapper;

        public AddWatcherHandler(
            MainDbContext mainDbContext,
            IRepository<Watcher> watcherRepository,
            IRepository<User> userRepository,
            CacheService cacheService,
            ILogger<AddWatcherRequest> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _watcherRepository = watcherRepository;
            _userRepository = userRepository;
            _cacheService = cacheService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<WatcherResponse> Handle(AddWatcherRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userRepository.GetById(request.UserId);

            // Get currencies
            var currencies = await _cacheService.GetFromCache<Currency>();

            // Get currency
            var currency =  currencies.FirstOrDefault(x => x.Id == request.CurrencyId);

            // Throw NotFound exception if it does not exist
            if (currency == null) throw new ConflictException(CurrencyMessage.CurrencyNotFound);

            // Get user watchers
            var watcher = await _watcherRepository.GetSingle(
                WatcherExpression.UniqueWatcher(
                    request.UserId,
                    request.CurrencyId,
                    request.IndicatorType));

            // Check if watcher exists
            if (watcher != null) throw new ConflictException(WatcherMessage.WatcherExists);

            // Add watcher
             watcher = new Watcher(
                user.Id,
                currency.Id,
                request.IndicatorType,
                IndicatorBuilder.BuildValue(currency, request.IndicatorType, currencies),
                request.Settings,
                new WatcherSettings(0, 0),
                false);
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
