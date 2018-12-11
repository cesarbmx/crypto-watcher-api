using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetAllLinesRequest: IRequest<List<LineResponse>>
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
    }
}
