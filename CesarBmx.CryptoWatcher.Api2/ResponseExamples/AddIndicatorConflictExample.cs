using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.ResponseExamples
{
    public class AddIndicatorConflictExample: IExamplesProvider<Conflict<AddIndicatorConflict>>
    {
        public Conflict<AddIndicatorConflict> GetExamples()
        {
            return FakeAddIndicatorConflict.GetFake();
        }
    }
}