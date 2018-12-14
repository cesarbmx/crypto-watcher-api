using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetIndicatorRequest : IRequest<IndicatorResponse>
    {
        public string IndicatorId { get; set; }
    }
}
