using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class AddWatcherConflictExample: IExamplesProvider<AddWatcherConflict>
    {
        public AddWatcherConflict GetExamples()
        {
            return FakeAddWatcherConflict.GetFake();
        }
    }
}