using System.Collections.Generic;
using CryptoWatcher.Application.Users.Responses;

namespace CryptoWatcher.Application.Users.FakeResponses
{
    public static class UserFakeResponse
    {
        public static UserResponse GetFake_Cesarbmx()
        {
            return new UserResponse
            {
                UserId = "johny.melavo"
            };
        }
        public static UserResponse GetFake_Johny()
        {
            return new UserResponse
            {
                UserId = "johny.melavo"
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
