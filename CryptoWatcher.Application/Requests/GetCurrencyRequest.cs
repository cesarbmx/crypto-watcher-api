using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetCurrencyRequest : IRequest<CurrencyResponse>
    {
        public string CurrencyId { get; set; }
    }
}
