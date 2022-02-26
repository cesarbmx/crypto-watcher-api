using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class AddWatcherConflictExample: IExamplesProvider<Conflict<AddWatcherConflictReason>>
    {
        public Conflict<AddWatcherConflictReason> GetExamples()
        {
            return FakeAddWatcherConflict.GetFake();
        }
    }
}