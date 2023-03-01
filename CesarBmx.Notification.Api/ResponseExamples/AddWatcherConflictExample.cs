using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class AddWatcherConflictExample: IExamplesProvider<Conflict<AddWatcherConflict>>
    {
        public Conflict<AddWatcherConflict> GetExamples()
        {
            return FakeAddWatcherConflict.GetFake();
        }
    }
}