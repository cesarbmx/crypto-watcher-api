using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetOrderRequest : IRequest<OrderResponse>
    {
        public string OrderId { get; set; }
    }
}
