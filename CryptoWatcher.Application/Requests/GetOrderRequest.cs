using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetOrderRequest : IRequest<OrderResponse>
    {
        public string OrderId { get; set; }
    }
}
