using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class MonitorWatchersJob
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<MonitorWatchersJob> _logger;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Order> _orderRepository;
        public MonitorWatchersJob(
            MainDbContext mainDbContext,
            ILogger<MonitorWatchersJob> logger,
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
                // Get all watchers
                var watchers = await _watcherRepository.GetAll();

                // Get all orders
                var orders = await _orderRepository.GetAll();

                // Build new orders
                var newOrders = watchers.BuildNewOrders(orders);

                // Add new orders
                _orderRepository.AddRange(newOrders);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Log into Splunk
                _logger.LogSplunkInformation(nameof(MonitorWatchersJob));

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
               // Log into Splunk 
                _logger.LogSplunkError(nameof(MonitorWatchersJob), ex);
            }
        }
    }
}