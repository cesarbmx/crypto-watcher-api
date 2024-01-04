using AutoMapper;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
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
    public class ConfirmOrderConsumer : IConsumer<OrderPlaced>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ConfirmOrderConsumer> _logger;
        private readonly ActivitySource _activitySource;

        public ConfirmOrderConsumer(
            MainDbContext mainDbContext,
            IMapper mapper,
            ILogger<ConfirmOrderConsumer> logger,
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
                using var span = _activitySource.StartActivity(nameof(ConfirmOrderConsumer));

                // Event
                var orderPlaced = context.Message;

                // Get watcher
                Watcher watcher = null;
                if (orderPlaced.OrderType == OrderType.BUY) watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(x => x.BuyingOrder.OrderId == orderPlaced.OrderId);
                if (orderPlaced.OrderType == OrderType.SELL) watcher = await _mainDbContext.Watchers.FirstOrDefaultAsync(x => x.SellingOrder.OrderId == orderPlaced.OrderId);

                // Return if no watcher was found
                if (watcher == null) return;

                // Confirm order
                if (orderPlaced.OrderType == OrderType.BUY) watcher.ConfirmBuyOrder(orderPlaced.Price, orderPlaced.PlacedAt);
                if (orderPlaced.OrderType == OrderType.SELL) watcher.ConfirmSellOrder(orderPlaced.Price, orderPlaced.PlacedAt);               

                // Update
                _mainDbContext.Watchers.Update(watcher);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@ExecutionTime}", nameof(ConfirmOrderConsumer), Guid.NewGuid(), stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
