using System.Collections.Generic;
using CryptoWatcher.Api.Responses;

namespace CryptoWatcher.Api.FakeResponses
{
    public static class UserFakeResponse
    {
        public static UserResponse GetFake_Cesarbmx()
        {
            return new UserResponse
            {
                UserId = "cesarbmx"
            };
        }
        public static UserResponse GetFake_Johny()
        {
            return new UserResponse
            {
                UserId = "johny"
            };
        }
        public static List<UserResponse> GetFake_List()
        {
            return new List<UserResponse>
            {
                GetFake_Cesarbmx(),
                GetFake_Johny()
            };
        }
    }
}
