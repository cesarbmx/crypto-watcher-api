using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class UserLogListResponseExample : IExamplesProvider<List<UserLogResponse>>
    {
        public List<UserLogResponse> GetExamples()
        {
            return FakeUserLog.GetFake_List();
        }
    }
}