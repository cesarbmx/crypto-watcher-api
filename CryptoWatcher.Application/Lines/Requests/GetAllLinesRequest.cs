using System.Collections.Generic;
using CryptoWatcher.Application.Lines.Responses;
using MediatR;

namespace CryptoWatcher.Application.Lines.Requests
{
    public class GetAllLinesRequest: IRequest<List<LineResponse>>
    {
        public string CurrencyId { get; set; }
        public string IndicatorId { get; set; }
    }
}
