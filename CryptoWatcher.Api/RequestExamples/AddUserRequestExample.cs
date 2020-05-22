using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddUserRequestExample : IExamplesProvider<AddUser>
    {
        public AddUser GetExamples()
        {
            return AddUserFakeRequest.GetFake_master();
        }
    }
}