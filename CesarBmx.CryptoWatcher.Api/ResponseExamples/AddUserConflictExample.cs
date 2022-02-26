using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class AddUserConflictExample: IExamplesProvider<Conflict<AddUserConflictReason>>
    {
        public Conflict<AddUserConflictReason> GetExamples()
        {
            return FakeAddUserConflict.GetFake();
        }
    }
}