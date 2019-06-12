using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateOrdersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateOrdersJob> _logger;
        public UpdateOrdersJob(
            MainDbContext mainDbContext,
            ILogger<UpdateOrdersJob> logger)
        {
            _mainDbContext = mainDbContext;
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

                // Get all watchers with buy or sells
                var watchers = await _mainDbContext.Watchers.Where(WatcherExpression.WatcherWillingToBuyOrSell()).ToListAsync();

                // Get all orders
                var orders = await _mainDbContext.Orders.ToListAsync();

                // Build new orders
                var newOrders = OrderBuilder.BuildNewOrders(watchers, orders);

                // Add
                _mainDbContext.Orders.AddRange(newOrders);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkJob(new
                {
                    newOrders.Count,
                    ExecutionTime = stopwatch.Elapsed.TotalSeconds
                });

                // Return
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log into Splunk 
                _logger.LogSplunkJob(new
                {
                    JobFailed = ex.Message
                });

                // Log error into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}