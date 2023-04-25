using AutoMapper;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.CryptoWatcher.Persistence.Contexts;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly OrderService _orderService;

        public OrderCancelledConsumer(
            MainDbContext mainDbContext,
            IMapper mapper,
            ILogger<OrderCancelledConsumer> logger,
            ActivitySource activitySource,
            IPublishEndpoint publishEndpoint,
            OrderService orderService)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _logger = logger;
            _activitySource = activitySource;
            _publishEndpoint = publishEndpoint;
            _orderService = orderService;
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

                // Order
                var order = await _mainDbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == context.Message.OrderId);

                // TODO: NotFound

                // Mark as cancelled
                order.MarkAsCancelled();

                // Save
                await _mainDbContext.SaveChangesAsync();

                // Stop watch
                stopwatch.Stop();

                // Log
                _logger.LogInformation("{@Event}, {@Id}, {@ExecutionTime}", nameof(OrderCancelled), Guid.NewGuid(), stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception ex)
            {
                // Log
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
