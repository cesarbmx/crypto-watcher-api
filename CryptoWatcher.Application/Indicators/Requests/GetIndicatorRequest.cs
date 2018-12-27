using CryptoWatcher.Application.Indicators.Responses;
using MediatR;

namespace CryptoWatcher.Application.Indicators.Requests
{
    public class GetIndicatorRequest : IRequest<IndicatorResponse>
    {
        public string IndicatorId { get; set; }
    }
}
