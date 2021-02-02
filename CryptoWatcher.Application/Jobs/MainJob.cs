using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CesarBmx.Shared.Logging.Extensions;
using CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;


namespace CryptoWatcher.Application.Jobs
{
    public class MainJob
    {
        private readonly CurrencyService _currencyService;
        private readonly IndicatorService _indicatorService;
        private readonly LineService _lineService;
        private readonly WatcherService _watcherService;
        private readonly OrderService _orderService;
        private readonly NotificationService _notificationService;
        private readonly ILogger<MainJob> _logger;
        public MainJob(
            CurrencyService currencyService,
            IndicatorService indicatorService,
            LineService lineService,
            WatcherService watcherService,
            OrderService orderService,
            NotificationService notificationService,
            ILogger<MainJob> logger)
        {
            _currencyService = currencyService;
            _indicatorService = indicatorService;
            _lineService = lineService;
            _watcherService = watcherService;
            _orderService = orderService;
            _notificationService = notificationService;
            _logger = logger;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Run
                var currencies = await _currencyService.UpdateCurrencies();
                var indicators = await _indicatorService.UpdateDependencyLevels();
                var lines = await _lineService.AddLines(currencies, indicators);
                var defaultWatchers = await _watcherService.UpdateDefaultWatchers(lines);
                var watchers = await _watcherService.UpdateWatchers(defaultWatchers);
                var orders = await _orderService.AddOrders(watchers);
                await _notificationService.AddOrderNotifications(orders);
                await _notificationService.SendTelegramNotifications();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation("Main", new
                {
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkInformation("Main", new
                {
                    Failed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}