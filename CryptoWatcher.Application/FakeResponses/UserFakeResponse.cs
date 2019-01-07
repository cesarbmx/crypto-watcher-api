using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class UserFakeResponse
    {
        public static UserResponse GetFake_Cesarbmx()
        {
            return new UserResponse
            {
                UserId = "johny12"
            };
        }
        public static UserResponse GetFake_Johny()
        {
            return new UserResponse
            {
                UserId = "johny12"
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
