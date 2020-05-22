using System.Collections.Generic;
using CryptoWatcher.Application.Responses;

namespace CryptoWatcher.Application.FakeResponses
{
    public static class FakeUser
    {
        public static User GetFake_master()
        {
            return new User
            {
                UserId = "master"
            };
        }
        public static User GetFake_cesar12()
        {
            return new User
            {
                UserId = "cesar12"
            };
        }
        public static List<User> GetFake_List()
        {
            return new List<User>
            {
                GetFake_master(),
                GetFake_cesar12()
            };
        }
    }
}
