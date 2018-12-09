using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetIndicatorRequest : IRequest<IndicatorResponse>
    {
        public string IndicatorId { get; set; }
    }
}
