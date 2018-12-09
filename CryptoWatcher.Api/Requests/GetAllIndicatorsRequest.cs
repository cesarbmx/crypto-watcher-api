using System.Collections.Generic;
using CryptoWatcher.Api.Responses;
using MediatR;

namespace CryptoWatcher.Api.Requests
{
    public class GetAllIndicatorsRequest: IRequest<List<IndicatorResponse>>
    {
        public string UserId { get; set; }
        public string IndicatorId { get; set; }
    }
}
