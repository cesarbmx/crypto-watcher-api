using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class EnableWatcherConflictExample : IExamplesProvider<Conflict<EnableWatcherConflict>>
    {
        public Conflict<EnableWatcherConflict> GetExamples()
        {
            return FakeEnableWatcherConflict.GetFake();
        }
    }
}