using System.Collections.Generic;
using CryptoWatcher.Application.Currencies.Responses;
using MediatR;

namespace CryptoWatcher.Application.Currencies.Requests
{
    public class GetAllCurrenciesRequest: IRequest<List<CurrencyResponse>>
    {
        
    }
}
