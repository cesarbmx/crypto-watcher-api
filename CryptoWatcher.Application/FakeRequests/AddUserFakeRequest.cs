using CryptoWatcher.Application.Requests;


namespace CryptoWatcher.Application.FakeRequests
{
    public static class AddUserFakeRequest
    {
        public static AddUser GetFake_master()
        {
            return new AddUser
            {
                UserId = "master"
            };
        }       
    }
}
