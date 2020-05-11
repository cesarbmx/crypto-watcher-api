using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddUserRequestExample : IExamplesProvider<AddUserRequest>
    {
        public AddUserRequest GetExamples()
        {
            return AddUserFakeRequest.GetFake_master();
        }
    }
}