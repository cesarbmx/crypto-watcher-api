using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class UpdateWatcherExample : IExamplesProvider<UpdateWatcher>
    {
        public UpdateWatcher GetExamples()
        {
            return UpdateWatcherFakeRequest.GetFake_1();
        }
    }
}