using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
        private readonly ActivitySource _activitySource;

        public MainJob(
            CurrencyService currencyService,
            IndicatorService indicatorService,
            LineService lineService,
            WatcherService watcherService,
            OrderService orderService,
            NotificationService notificationService,
            ILogger<MainJob> logger,
            ActivitySource activitySource)
        {
            _currencyService = currencyService;
            _indicatorService = indicatorService;
            _lineService = lineService;
            _watcherService = watcherService;
            _orderService = orderService;
            _notificationService = notificationService;
            _logger = logger;
            _activitySource = activitySource;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {             
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Start span
                using var span = _activitySource.StartActivity(nameof(MainJob));

                // Main job
                var currencies = await _currencyService.ImportCurrencies();
                var indicators = await _indicatorService.UpdateDependencyLevels();
                var lines = await _lineService.CreateNewLines(currencies, indicators);
                var defaultWatchers = await _watcherService.UpdateDefaultWatchers(lines);
                var watchers = await _watcherService.UpdateWatchers(defaultWatchers);
                var orders = await _orderService.AddOrders(watchers);
                await _orderService.SubmitOrders(orders);
                //orders = await _orderService.ProcessOrders(orders, watchers);
                var notifications = await _notificationService.CreateNotifications(orders);
                await _notificationService.SendTelegramNotifications(notifications);
                await _lineService.DeleteObsoleteLines();

                // Stop watch
                stopwatch.Stop();

                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@ExecutionTime}", "MainJobFinished", Guid.NewGuid(), stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}