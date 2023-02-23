using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.ResponseExamples
{
    public class AddWatcherConflictExample: IExamplesProvider<Conflict<AddWatcherConflict>>
    {
        public Conflict<AddWatcherConflict> GetExamples()
        {
            return FakeAddWatcherConflict.GetFake();
        }
    }
}