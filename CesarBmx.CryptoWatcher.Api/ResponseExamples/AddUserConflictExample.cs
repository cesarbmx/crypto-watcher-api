using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class AddUserConflictExample: IExamplesProvider<AddUserConflict>
    {
        public AddUserConflict GetExamples()
        {
            return FakeAddUserConflict.GetFake();
        }
    }
}