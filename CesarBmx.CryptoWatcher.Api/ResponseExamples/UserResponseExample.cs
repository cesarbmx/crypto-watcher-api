using System.Collections.Generic;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Resources;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class UserResponseExample : IExamplesProvider<User>
    {
        public User GetExamples()
        {
            return FakeUser.GetFake_master();
        }
    }
    public class UserListResponseExample : IExamplesProvider<List<User>>
    {
        public List<User> GetExamples()
        {
            return FakeUser.GetFake_List();
        }
    }
}