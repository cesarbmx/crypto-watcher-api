using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class ResponseExample : IExamplesProvider<WatcherResponse>
    {
        public WatcherResponse GetExamples()
        {
            return FakeWatcher.GetFake_master_Bitcoin_RSI();
        }
    }
    public class ResponseListExample : IExamplesProvider<List<WatcherResponse>>
    {
        public List<WatcherResponse> GetExamples()
        {
            return FakeWatcher.GetFake_List();
        }
    }
}