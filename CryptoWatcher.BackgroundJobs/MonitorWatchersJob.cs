using System;
using System.Threading.Tasks;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class MonitorWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<MonitorWatchersJob> _logger;
        private readonly OrderService _orderService;

        public MonitorWatchersJob(
            MainDbContext mainDbContext,
            ILogger<MonitorWatchersJob> logger,
            OrderService orderService)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _orderService = orderService;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Execute()
        {
            try
            {
                // Add orders from watchers ready to buy or sell
                await _orderService.AddOrdersFromWatchers();

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogSplunkInformation(nameof(LoggingEvents.OrdersAdded));

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
               // Log into Splunk 
                _logger.LogSplunkError(nameof(LoggingEvents.AddingOrdersFailed), ex.Message);
            }
        }
    }
}