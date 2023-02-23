using CesarBmx.CryptoWatcher.Application.FakeRequests;
using CesarBmx.CryptoWatcher.Application.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.RequestExamples
{
    public class AddUserExample : IExamplesProvider<AddUser>
    {
        public AddUser GetExamples()
        {
            return FakeAddUser.GetFake_master();
        }
    }
}