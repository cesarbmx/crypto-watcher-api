using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CesarBmx.Shared.Logging.Extensions;
using CesarBmx.CryptoWatcher.Application.Services;
using Hangfire;
using Microsoft.Extensions.Logging;


namespace CesarBmx.CryptoWatcher.Application.Jobs
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
                var currencies = await _currencyService.ImportCurrencies();
                var indicators = await _indicatorService.UpdateDependencyLevels();
                var lines = await _lineService.CreateNewLines(currencies, indicators);
                var defaultWatchers = await _watcherService.SetDefaultWatchers(lines);
                var watchers = await _watcherService.UpdateWatchers(defaultWatchers);
                var orders = await _orderService.AddOrders(watchers);
                orders = await _orderService.ProcessOrders(orders, watchers);
                var notifications = await _notificationService.AddNotifications(orders);
                await _notificationService.SendTelegramNotifications(notifications);
                await _lineService.RemoveObsoleteLines();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(nameof(MainJob), new
                {
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkInformation(nameof(MainJob), new
                {
                    Failed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}