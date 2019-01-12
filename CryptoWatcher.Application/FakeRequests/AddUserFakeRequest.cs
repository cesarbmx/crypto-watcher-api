using CryptoWatcher.Application.Requests;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddUserFakeRequest
    {
        public static AddUserRequest GetFake_master()
        {
            return new AddUserRequest
            {
                UserId = "master"
            };
        }       
    }
}
