using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class WatcherResponseExample : IExamplesProvider<WatcherResponse>
    {
        public WatcherResponse GetExamples()
        {
            return WatcherFakeResponse.GetFake_master_Bitcoin_RSI();
        }
    }
    public class WatcherListResponseExample : IExamplesProvider<List<WatcherResponse>>
    {
        public List<WatcherResponse> GetExamples()
        {
            return WatcherFakeResponse.GetFake_List();
        }
    }
}