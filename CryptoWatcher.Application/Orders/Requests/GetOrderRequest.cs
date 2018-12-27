using System;
using CryptoWatcher.Application.Orders.Responses;
using MediatR;

namespace CryptoWatcher.Application.Orders.Requests
{
    public class GetOrderRequest : IRequest<OrderResponse>
    {
        public Guid OrderId { get; set; }
    }
}
