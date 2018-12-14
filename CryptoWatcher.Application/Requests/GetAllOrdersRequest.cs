using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetAllOrdersRequest : IRequest<List<OrderResponse>>
    {
        public string UserId { get; set; }
    }
}
