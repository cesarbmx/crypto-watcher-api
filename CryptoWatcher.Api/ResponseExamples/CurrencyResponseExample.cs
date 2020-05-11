using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class CurrencyResponseExample : IExamplesProvider<CurrencyResponse>
    {
        public CurrencyResponse GetExamples()
        {
            return CurrencyFakeResponse.GetFake_Bitcoin();
        }
    }
    public class CurrencyListResponseExample : IExamplesProvider<List<CurrencyResponse>>
    {
        public List<CurrencyResponse> GetExamples()
        {
            return CurrencyFakeResponse.GetFake_List();
        }
    }
}