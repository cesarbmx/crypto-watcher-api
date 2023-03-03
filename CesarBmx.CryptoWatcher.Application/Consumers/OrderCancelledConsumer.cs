using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        readonly ILogger<OrderCancelledConsumer> _logger;

        public OrderCancelledConsumer(ILogger<OrderCancelledConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCancelled> context)
        {
            _logger.LogInformation("Order cancelled");

            return Task.CompletedTask;
        }
    }

}
