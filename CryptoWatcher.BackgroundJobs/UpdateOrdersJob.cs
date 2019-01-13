using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Persistence.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateOrdersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<UpdateOrdersJob> _logger;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Order> _orderRepository;
        public UpdateOrdersJob(
            MainDbContext mainDbContext,
            ILogger<UpdateOrdersJob> logger,
            IRepository<Watcher> watcherRepository,
            IRepository<Order> orderRepository)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _watcherRepository = watcherRepository;
            _orderRepository = orderRepository;
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
                var watchers = await _watcherRepository.GetAll(WatcherExpression.WatcherWillingToBuyOrSell());

                // Get all orders
                var orders = await _orderRepository.GetAll();

                // Build new orders
                var newOrders = OrderBuilder.BuildNewOrders(watchers, orders);

                // Add
                _orderRepository.AddRange(newOrders);

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
                _logger.LogSplunkError(ex);
            }
        }
    }
}