using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class OrderAddedConsumer : IConsumer<OrderAdded>
    {
        readonly ILogger<OrderAddedConsumer> _logger;

        public OrderAddedConsumer(ILogger<OrderAddedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderAdded> context)
        {
            _logger.LogInformation("Hola");

            return Task.CompletedTask;
        }
    }

}
