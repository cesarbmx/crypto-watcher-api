using CesarBmx.Shared.Messaging.CryptoWatcher.Commands;
using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class AddOrderConsumer : IConsumer<AddOrder>
    {
        readonly ILogger<AddOrderConsumer> _logger;

        public AddOrderConsumer(ILogger<AddOrderConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<AddOrder> context)
        {
            _logger.LogInformation("Add order");

            // Publish OrderAdded
            var orderAdded = new OrderAdded { OrderId = context.Message.OrderId };
            context.Publish(orderAdded);

            return Task.CompletedTask;
        }
    }
}
