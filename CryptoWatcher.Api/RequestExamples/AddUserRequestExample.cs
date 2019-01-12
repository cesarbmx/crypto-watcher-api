using CryptoWatcher.Application.FakeRequests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddUserRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return AddUserFakeRequest.GetFake_master();
        }
    }
}