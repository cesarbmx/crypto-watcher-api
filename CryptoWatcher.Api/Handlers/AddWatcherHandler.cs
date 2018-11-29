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
        private readonly UserService _userService;
        private readonly CurrencyService _currencyService;
        private readonly ILogger<AddWatcherHandler> _logger;
        private readonly IMapper _mapper;

        public AddWatcherHandler(
            MainDbContext mainDbContext,
            IRepository<Watcher> watcherRepository,
            UserService userService,
            CurrencyService currencyService,
            ILogger<AddWatcherHandler> logger,
            IMapper mapper)
        {
            _mainDbContext = mainDbContext;
            _watcherRepository = watcherRepository;
            _userService = userService;
            _currencyService = currencyService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<WatcherResponse> Handle(AddWatcherRequest request, CancellationToken cancellationToken)
        {
            // Get user
            var user = await _userService.GetUser(request.UserId);

            // Get currencies
            var currency = await _currencyService.GetCurrency(request.CurrencyId);

            // Get user watchers
            var watcher = await _watcherRepository.GetSingle(
                WatcherExpression.UniqueWatcher(
                    request.UserId,
                    request.CurrencyId,
                    request.IndicatorType));

            // Check if watcher exists
            if (watcher != null) throw new ConflictException(WatcherMessage.WatcherExists);

            // Get currencies
            var currencies = await _currencyService.GetCurrencies();

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
            _logger.LogSplunkInformation(nameof(LoggingEvents.WatcherAdded), watcher);

            // Response
            var response = _mapper.Map<WatcherResponse>(watcher);

            // Return
            return response;
        }
    }
}
