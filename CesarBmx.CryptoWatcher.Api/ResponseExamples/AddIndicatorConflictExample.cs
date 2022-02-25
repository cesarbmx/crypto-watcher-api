using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class AddIndicatorConflictExample: IExamplesProvider<AddIndicatorConflict>
    {
        public AddIndicatorConflict GetExamples()
        {
            return FakeAddIndicatorConflict.GetFake();
        }
    }
}