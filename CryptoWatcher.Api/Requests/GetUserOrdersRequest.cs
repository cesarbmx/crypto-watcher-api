using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetUserOrdersRequest : IRequest<List<OrderResponse>>
    {
        public string UserId { get; set; }
    }
}
