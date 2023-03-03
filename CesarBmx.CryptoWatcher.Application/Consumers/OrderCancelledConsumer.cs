using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using CesarBmx.Shared.Messaging.Notification.Commands;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task Consume(ConsumeContext<OrderCancelled> context)
        {
            _logger.LogInformation("Order cancelled");

            var sendMessage = new SendMessage { MessageId = Guid.NewGuid(), Text = "Order cancelled" };
            await context.Publish(sendMessage);
        }
    }

}
