using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class AddIndicatorConflictExample: IExamplesProvider<Conflict<AddIndicatorConflict>>
    {
        public Conflict<AddIndicatorConflict> GetExamples()
        {
            return FakeAddIndicatorConflict.GetFake();
        }
    }
}