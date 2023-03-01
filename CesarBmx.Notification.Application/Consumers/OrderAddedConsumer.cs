using CesarBmx.Shared.Messaging.Notification.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CesarBmx.Notification.Application.Consumers
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
