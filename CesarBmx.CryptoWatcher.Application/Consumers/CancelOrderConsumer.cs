using CesarBmx.Shared.Messaging.CryptoWatcher.Commands;
using CesarBmx.Shared.Messaging.CryptoWatcher.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CesarBmx.CryptoWatcher.Application.Consumers
{
    public class CancelOrderConsumer : IConsumer<CancelOrder>
    {
        readonly ILogger<CancelOrderConsumer> _logger;

        public CancelOrderConsumer(ILogger<CancelOrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CancelOrder> context)
        {
            _logger.LogInformation("Cancel order");

            var orderCancelled = new OrderCancelled { OrderId = context.Message.OrderId };
            await context.Publish(orderCancelled);

            await context.RespondAsync(orderCancelled);
        }
    }

}
