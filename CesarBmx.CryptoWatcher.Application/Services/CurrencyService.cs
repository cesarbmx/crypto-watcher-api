using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CesarBmx.Shared.Application.Exceptions;
using CesarBmx.Shared.Persistence.Extensions;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using AutoMapper.QueryableExtensions;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Types;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Messaging.Ordering.Commands;
using MassTransit;
using CesarBmx.Shared.Messaging.Notification.Commands;

namespace CesarBmx.CryptoWatcher.Application.Services
{
    public class CurrencyService
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyService> _logger;
        private readonly CoinpaprikaAPI.Client _coinpaprikaClient;
        private readonly ActivitySource _activitySource;
        private readonly IBus _bus;


        public CurrencyService(
            MainDbContext mainDbContext,
            IMapper mapper,
            ILogger<CurrencyService> logger,
            CoinpaprikaAPI.Client coinpaprikaClient,
            ActivitySource activitySource,
            IBus bus)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _logger = logger;
            _coinpaprikaClient = coinpaprikaClient;
            _activitySource = activitySource;
            _bus = bus;
        }

        public async Task<List<Responses.Currency>> GetCurrencies()
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(GetCurrencies));

            // Get all currencies
            var currencies = await _mainDbContext.Currencies
                .ProjectTo<Responses.Currency>(_mapper.ConfigurationProvider)
                .ToListAsync();

            // Return
            return currencies;
        }
        public async Task AddOrder()
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(AddOrder));

            // Time
            var now = DateTime.UtcNow.StripSeconds();

            // New order
            var order = new Order(1, "master", "BTC", 30000, 1, OrderType.BUY, now);

            // Add order
            await _mainDbContext.Orders.AddAsync(order);

            // Command
            var submitOrder = _mapper.Map<SubmitOrder>(order);

            // Send
            await _bus.Send(submitOrder);

            // New notification
            var notification = new Notification("master", "666555444", "Order submitted", now);

            // Add notification
            await _mainDbContext.Notifications.AddAsync(notification);

            // Command
            var sendNotification = _mapper.Map<SendMessage>(notification);

            // Send
            await _bus.Send(sendNotification);

            // Save
            await _mainDbContext.SaveChangesAsync();
        }
        public async Task<Responses.Currency> GetCurrency(string currencyId)
        {
            // Start span
            using var span = _activitySource.StartActivity(nameof(GetCurrency));

            // Get currency
            var currency = await _mainDbContext.Currencies.FindAsync(currencyId);

            // Currency not found
            if (currency == null) throw new NotFoundException(CurrencyMessage.CurrencyNotFound);

            // Response
            var response = _mapper.Map<Responses.Currency>(currency);

            // Return
            return response;
        }
        public async Task<List<Currency>> ImportCurrencies()
        {
            // Start watch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Start span
            using var span = _activitySource.StartActivity(nameof(ImportCurrencies));                  

            // Get all currencies from CoinMarketCap
            var result = await _coinpaprikaClient.GetTickersAsync();

            var tickers = result.Value.Where(x =>
                x.Symbol == "BTC" ||
                x.Symbol == "XRP" ||
                x.Symbol == "ETH" ||
                x.Symbol == "BCH" ||
                x.Symbol == "XML" ||
                x.Symbol == "EOS" ||
                x.Symbol == "ADA").GroupBy(x=>x.Symbol).Select(x=>x.First()).ToList();

            // Build currencies
            var newCurrencies = _mapper.Map<List<Currency>>(tickers);

            // Get all currencies
            var currencies = await _mainDbContext.Currencies.ToListAsync();

            // Update 
            _mainDbContext.UpdateCollection(currencies, newCurrencies);

            // Save
            await _mainDbContext.SaveChangesAsync();

            // Stop watch
            stopwatch.Stop();

            // Log
            _logger.LogInformation("{@Event}, {@Id}, {@Count}, {@ExecutionTime}", "CurrenciesImported", Guid.NewGuid(), newCurrencies.Count, stopwatch.Elapsed.TotalSeconds);

            // Return
            return newCurrencies;
        }
    }
}
