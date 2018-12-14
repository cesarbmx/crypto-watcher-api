using System.Collections.Generic;
using CryptoWatcher.Application.Responses;
using MediatR;

namespace CryptoWatcher.Application.Requests
{
    public class GetCurrenciesRequest: IRequest<List<CurrencyResponse>>
    {
        
    }
}
