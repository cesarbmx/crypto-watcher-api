using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class SetWatcherConflictExample : IExamplesProvider<SetWatcherConflict>
    {
        public SetWatcherConflict GetExamples()
        {
            return FakeConflict.GetFake_SetWatcherConflict();
        }
    }
}