using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class UserResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return UserFakeResponse.GetFake_master();
        }
    }
    public class UserListResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return UserFakeResponse.GetFake_List();
        }
    }
}