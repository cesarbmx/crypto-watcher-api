using CryptoWatcher.Application.System.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
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
    public class ValidationFailedExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ErrorFakeResponse.GetFake_ValidationFailed();
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