using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using CesarBmx.Shared.Messaging.Notification.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task Consume(ConsumeContext<OrderAdded> context)
        {
            _logger.LogInformation("Order added");

            var sendMessage = new SendMessage { MessageId = Guid.NewGuid(), Text = "Order added" };
            await context.Publish(sendMessage);
        }
    }

}
