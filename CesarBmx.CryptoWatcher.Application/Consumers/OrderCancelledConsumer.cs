﻿using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Domain.Models;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
using CesarBmx.Shared.Common.Extensions;
using CesarBmx.Shared.Messaging.Notification.Commands;
using CesarBmx.Shared.Messaging.Ordering.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        private readonly MainDbContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderCancelledConsumer> _logger;
        private readonly ActivitySource _activitySource;

        public OrderCancelledConsumer(
            MainDbContext mainDbContext,
            IMapper mapper,
            ILogger<OrderCancelledConsumer> logger,
            ActivitySource activitySource)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _logger = logger;
            _activitySource = activitySource;
        }

        public async Task Consume(ConsumeContext<OrderCancelled> context)
        {
            try
            {
                // Start watch
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Start span
                using var span = _activitySource.StartActivity(nameof(OrderPlaced));

                // Event
                var orderCancelled = context.Message; 

                // Order
                var order = await _mainDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == orderCancelled.OrderId);

                // Mark as cancelled
                order.MarkAsCancelled();

                // Time
                var now = DateTime.UtcNow.StripSeconds();

                // New notification
                var notification = new Notification("master", "666555444", "Order cancelled", now);

                // Add notification
                await _mainDbContext.Notifications.AddAsync(notification);

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Response
                await context.RespondAsync(order);

                // Stop watch
                stopwatch.Stop();

                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@ExecutionTime}", nameof(OrderCancelled), Guid.NewGuid(), stopwatch.Elapsed.TotalSeconds);

                // Command
                var sendNotification = _mapper.Map<SendMessage>(notification);

                // Send
                await context.Send(sendNotification);
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
