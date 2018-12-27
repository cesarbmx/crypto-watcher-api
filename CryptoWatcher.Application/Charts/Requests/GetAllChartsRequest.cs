using System.Collections.Generic;
using CryptoWatcher.Application.Charts.Responses;
using MediatR;

namespace CryptoWatcher.Application.Charts.Requests
{
    public class GetAllChartsRequest : IRequest<List<ChartResponse>>
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
    }
}
