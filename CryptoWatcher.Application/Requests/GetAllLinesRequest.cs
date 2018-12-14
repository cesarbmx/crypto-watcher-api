using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetAllLinesRequest: IRequest<List<LineResponse>>
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
    }
}
