using CryptoWatcher.Shared.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Shared.ResponseExamples
{
    public class BadRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ErrorFakeResponse.GetFake_BadRequest();
        }
    }
    public class NotFoundExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ErrorFakeResponse.GetFake_NotFound();
        }
    }
    public class ConflictExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ErrorFakeResponse.GetFake_Conflict();
        }
    }
    public class InvalidRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ErrorFakeResponse.GetFake_InvalidRequest();
        }
    }
    public class InternalServerErrorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ErrorFakeResponse.GetFake_InternalServerError();
        }
    }
}