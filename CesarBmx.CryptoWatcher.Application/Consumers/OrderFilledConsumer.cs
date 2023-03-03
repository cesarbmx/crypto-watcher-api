using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class OrderFilledConsumer : IConsumer<OrderFilled>
    {
        readonly ILogger<OrderFilledConsumer> _logger;

        public OrderFilledConsumer(ILogger<OrderFilledConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderFilled> context)
        {
            _logger.LogInformation("Order filled");

            return Task.CompletedTask;
        }
    }

}
