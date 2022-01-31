using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class SetWatcherConflictExample : IExamplesProvider<Conflict<SetWatcherConflictReason>>
    {
        public Conflict<SetWatcherConflictReason> GetExamples()
        {
            return FakeConflict.GetFake_SetWatcherConflict();
        }
    }
}