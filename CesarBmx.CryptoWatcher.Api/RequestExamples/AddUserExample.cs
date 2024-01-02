using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.RequestExamples
{
    public class AddUserExample : IExamplesProvider<AddUserRequest>
    {
        public AddUserRequest GetExamples()
        {
            return FakeAddUser.GetFake_master();
        }
    }
}