using System.Collections.Generic;
using CryptoWatcher.Application.Orders.Responses;
using MediatR;

namespace CryptoWatcher.Application.Orders.Requests
{
    public class GetAllOrdersRequest : IRequest<List<OrderResponse>>
    {
        public string UserId { get; set; }
    }
}
