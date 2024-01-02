using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class UpdateWatcherExample : IExamplesProvider<SetWatcherRequest>
    {
        public SetWatcherRequest GetExamples()
        {
            return FakeSetWatcher.GetFake_1();
        }
    }
}