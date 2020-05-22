using System.Collections.Generic;
using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
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