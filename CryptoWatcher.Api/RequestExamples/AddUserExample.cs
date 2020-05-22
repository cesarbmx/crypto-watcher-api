using CryptoWatcher.Application.FakeRequests;
using CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.RequestExamples
{
    public class AddUserExample : IExamplesProvider<AddUser>
    {
        public AddUser GetExamples()
        {
            return AddUserFakeRequest.GetFake_master();
        }
    }
}