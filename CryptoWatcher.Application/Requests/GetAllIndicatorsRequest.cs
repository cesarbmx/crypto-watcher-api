using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetAllIndicatorsRequest: IRequest<List<IndicatorResponse>>
    {
        public string UserId { get; set; }
    }
}
