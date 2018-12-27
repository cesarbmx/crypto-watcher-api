using CryptoWatcher.Application.Currencies.Responses;
using MediatR;

namespace CryptoWatcher.Application.Currencies.Requests
{
    public class GetCurrencyRequest : IRequest<CurrencyResponse>
    {
        public string CurrencyId { get; set; }
    }
}
