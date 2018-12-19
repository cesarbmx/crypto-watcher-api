using System;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetOrderRequest : IRequest<OrderResponse>
    {
        public Guid OrderId { get; set; }
    }
}
