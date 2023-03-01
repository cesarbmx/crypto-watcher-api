using System.Collections.Generic;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
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