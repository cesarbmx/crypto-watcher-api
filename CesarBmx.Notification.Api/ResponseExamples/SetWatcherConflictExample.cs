using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class SetWatcherConflictExample : IExamplesProvider<Conflict<SetWatcherConflict>>
    {
        public Conflict<SetWatcherConflict> GetExamples()
        {
            return FakeSetWatcherConflict.GetFake();
        }
    }
}