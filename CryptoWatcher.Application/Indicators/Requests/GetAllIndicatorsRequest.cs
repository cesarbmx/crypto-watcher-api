using System.Collections.Generic;
using CryptoWatcher.Application.Indicators.Responses;
using MediatR;

namespace CryptoWatcher.Application.Indicators.Requests
{
    public class GetAllIndicatorsRequest: IRequest<List<IndicatorResponse>>
    {
        public string UserId { get; set; }
    }
}
