using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class CurrencyResponseExample : IExamplesProvider<Currency>
    {
        public Currency GetExamples()
        {
            return FakeCurrency.GetFake_Bitcoin();
        }
    }
    public class CurrencyListResponseExample : IExamplesProvider<List<Currency>>
    {
        public List<Currency> GetExamples()
        {
            return FakeCurrency.GetFake_List();
        }
    }
}