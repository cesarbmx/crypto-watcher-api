using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class AddIndicatorConflictExample: IExamplesProvider<Conflict<AddIndicatorConflictReason>>
    {
        public Conflict<AddIndicatorConflictReason> GetExamples()
        {
            return FakeAddIndicatorConflict.GetFake();
        }
    }
}