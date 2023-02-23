using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.ResponseExamples
{
    public class AddUserConflictExample: IExamplesProvider<Conflict<AddUserConflict>>
    {
        public Conflict<AddUserConflict> GetExamples()
        {
            return FakeAddUserConflict.GetFake();
        }
    }
}