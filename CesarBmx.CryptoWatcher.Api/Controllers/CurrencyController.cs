using CesarBmx.Shared.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.CryptoWatcher.Application.Services;
using CesarBmx.Shared.Api.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MassTransit;
using CesarBmx.Shared.Messaging.Notification.Commands;
using CesarBmx.Shared.Messaging.Ordering.Commands;
using CesarBmx.Shared.Messaging.Ordering.Events;

namespace CesarBmx.CryptoWatcher.Api.Controllers
{
    [SwaggerResponse(500, Type = typeof(InternalServerError))]
    [SwaggerResponse(401, Type = typeof(Unauthorized))]
    [SwaggerResponse(403, Type = typeof(Forbidden))]
    [SwaggerOrder(orderPrefix: "A")]
    public class CurrencyController : Controller
    {
        private readonly CurrencyService _currencyService;
        private readonly IBus _bus;
        private readonly IRequestClient<CancelOrder> _requestClient;

        public CurrencyController(CurrencyService currencyService, IBus bus, IRequestClient<CancelOrder> requestClient)
        {
            _currencyService = currencyService;
            _bus = bus;
            _requestClient = requestClient;
        }

        /// <summary>
        /// Get currencies
        /// </summary>
        [HttpGet]
        [Route("api/currencies")]
        [SwaggerResponse(200, Type = typeof(List<Currency>))]  
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            // Reponse
            var response = await _currencyService.GetCurrencies();

            ///////////// TEST /////////////

            // Place orders
            var placeOrder1 = new PlaceOrder 
            {
                OrderId = Guid.NewGuid(),
                CurrencyId = "BTC",
                OrderType = Shared.Messaging.Ordering.Types.OrderType.BUY,
                Price = 30000,
                Quantity = 1,
                UserId = "master",
                WatcherId = 1
            
            };
            await _bus.Send(placeOrder1);
            var placeOrder2 = new PlaceOrder {
                OrderId = Guid.NewGuid(),
                CurrencyId = "BTC",
                OrderType = Shared.Messaging.Ordering.Types.OrderType.BUY,
                Price = 40000,
                Quantity = 2,
                UserId = "master",
                WatcherId = 1
            };
            await _bus.Send(placeOrder2);

            // Cancel order
            var cancelOrder = new CancelOrder { OrderId = placeOrder1.OrderId };
            var result = await _requestClient.GetResponse<OrderCancelled>(cancelOrder);

            var sendMessage = new SendMessage{ MessageId = Guid.NewGuid(), Text = "Hello!"};
            await _bus.Send(sendMessage);

            // Send message

            ////////////////////////////////

            // Return
            return Ok(response);
        }

        /// <summary>
        /// Get currency
        /// </summary>
        [HttpGet]
        [Route("api/currencies/{currencyId}", Name = "Currencies_GetCurrency")]
        [SwaggerResponse(200, Type = typeof(Currency))]
        [SwaggerResponse(404, Type = typeof(NotFound))]
        [SwaggerOperation(Tags = new[] { "Currencies" }, OperationId = "Currencies_GetCurrency")]
        public async Task<IActionResult> GetCurrency(string currencyId)
        {
            // Reponse
            var response = await _currencyService.GetCurrency(currencyId);

            // Return
            return Ok(response);
        }
    }
}

