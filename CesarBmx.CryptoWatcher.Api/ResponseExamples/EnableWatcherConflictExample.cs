using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class EnableWatcherConflictExample : IExamplesProvider<Conflict<EnableWatcherConflictReason>>
    {
        public Conflict<EnableWatcherConflictReason> GetExamples()
        {
            return FakeEnableWatcherConflict.GetFake();
        }
    }
}