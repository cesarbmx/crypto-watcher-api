using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Builders;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Contexts;
using CryptoWatcher.Shared.Extensions;
using CryptoWatcher.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace CryptoWatcher.BackgroundJobs
{
    public class UpdateOrdersJob
    {
        private readonly IContext _context;
        private readonly ILogger<UpdateOrdersJob> _logger;
        private readonly IRepository<Watcher> _watcherRepository;
        private readonly IRepository<Order> _orderRepository;
        public UpdateOrdersJob(
            IContext context,
            ILogger<UpdateOrdersJob> logger,
            IRepository<Watcher> watcherRepository,
            IRepository<Order> orderRepository)
        {
            _context = context;
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

                // Get all watchers
                var watchers = await _watcherRepository.GetAll();

                // Get all orders
                var orders = await _orderRepository.GetAll();

                // Build new orders
                var newOrders = OrderBuilder.BuildNewOrders(watchers, orders);

                // Add
                _orderRepository.AddRange(newOrders);

                // Save
                await _context.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log into Splunk
                _logger.LogSplunkInformation(new
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
                _logger.LogSplunkError(ex);
            }
        }
    }
}