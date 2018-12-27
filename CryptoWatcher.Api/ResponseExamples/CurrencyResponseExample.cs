using CryptoWatcher.Application.Currencies.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class CurrencyResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return CurrencyFakeResponse.GetFake_Bitcoin();
        }
    }
    public class CurrencyListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return CurrencyFakeResponse.GetFake_List();
        }
    }
}