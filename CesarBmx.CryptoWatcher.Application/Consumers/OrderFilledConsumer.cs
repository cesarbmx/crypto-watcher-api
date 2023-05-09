using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Messaging.Notification.Commands;
using CesarBmx.Shared.Messaging.Ordering.Events;
using MassTransit;
using MassTransit.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class OrderFilledConsumer : IConsumer<OrderFilled>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderFilledConsumer> _logger;
        private readonly ActivitySource _activitySource;

        public OrderFilledConsumer(
            MainDbContext mainDbContext,
            IMapper mapper,
            ILogger<OrderFilledConsumer> logger,
            ActivitySource activitySource)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _logger = logger;
            _activitySource = activitySource;
        }

        public async Task Consume(ConsumeContext<OrderFilled> context)
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Start span
                using var span = _activitySource.StartActivity(nameof(OrderFilled));

                // Event
                var orderFilled = context.Message;

                // Order
                var order = await _mainDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderFilled.OrderId);

                // Mark as filled
                order.MarkAsFilled();

                // Time
                var now = DateTime.UtcNow.StripSeconds();

                // New notification
                var notification = new Notification("master", "666555444", "Order filled", now);

                // Add notification
                await _mainDbContext.Notifications.AddAsync(notification);

                // Command
                var sendNotification = _mapper.Map<SendMessage>(notification);

                // Send
                await context.Send(sendNotification);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@ExecutionTime}", nameof(OrderFilled), Guid.NewGuid(), stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
