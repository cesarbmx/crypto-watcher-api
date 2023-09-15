using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Domain.Builders;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Messaging.Notification.Commands;
using CesarBmx.Shared.Messaging.Ordering.Events;
using CesarBmx.Shared.Messaging.Ordering.Types;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class OrderPlacedConsumer : IConsumer<OrderPlaced>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderPlacedConsumer> _logger;
        private readonly ActivitySource _activitySource;

        public OrderPlacedConsumer(
            MainDbContext mainDbContext,
            IMapper mapper,
            ILogger<OrderPlacedConsumer> logger,
            ActivitySource activitySource)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _logger = logger;
            _activitySource = activitySource;
        }

        public async Task Consume(ConsumeContext<OrderPlaced> context)
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Start span
                using var span = _activitySource.StartActivity(nameof(OrderPlaced));

                // Event
                var orderPlaced = context.Message;

                // Add
                var watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(x => x.EntryOrderId == orderPlaced.OrderId || x.ExitOrderId == orderPlaced.OrderId);

                if (watcher == null) return;

                if (orderPlaced.OrderType == OrderType.BUY)
                {
                    watcher.SetAsBought();
                }
                else
                {
                    watcher.SetAsSold();
                }

                // Update
                _mainDbContext.Update(watcher);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@ExecutionTime}", nameof(OrderPlaced), Guid.NewGuid(), stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
