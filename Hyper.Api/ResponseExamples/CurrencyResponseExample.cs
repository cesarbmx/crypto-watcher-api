using Hyper.Api.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace Hyper.Api.ResponseExamples
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