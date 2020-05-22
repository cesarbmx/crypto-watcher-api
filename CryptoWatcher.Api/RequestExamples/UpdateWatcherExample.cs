using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class UpdateWatcherExample : IExamplesProvider<UpdateWatcher>
    {
        public UpdateWatcher GetExamples()
        {
            return UpdateWatcherFakeRequest.GetFake_1();
        }
    }
}