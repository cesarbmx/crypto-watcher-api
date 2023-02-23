using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.RequestExamples
{
    public class UpdateWatcherExample : IExamplesProvider<SetWatcher>
    {
        public SetWatcher GetExamples()
        {
            return FakeSetWatcher.GetFake_1();
        }
    }
}