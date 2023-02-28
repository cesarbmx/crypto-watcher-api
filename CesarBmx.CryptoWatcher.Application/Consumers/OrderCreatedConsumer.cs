using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCreated> context)
        {
            _logger.LogInformation("Hola");

            return Task.CompletedTask;
        }
    }

}
