using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class SetWatcherConflictExample : IExamplesProvider<Conflict<SetWatcherConflict>>
    {
        public Conflict<SetWatcherConflict> GetExamples()
        {
            return FakeSetWatcherConflict.GetFake();
        }
    }
}