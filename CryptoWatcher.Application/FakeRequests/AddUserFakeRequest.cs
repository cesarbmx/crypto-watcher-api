using CryptoWatcher.Application.Requests;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddUserFakeRequest
    {
        public static AddUserRequest GetFake_1()
        {
            return new AddUserRequest
            {
                UserId = "johny12"
            };
        }       
    }
}
