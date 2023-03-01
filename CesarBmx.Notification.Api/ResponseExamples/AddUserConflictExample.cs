using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Shared.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class AddUserConflictExample: IExamplesProvider<Conflict<AddUserConflict>>
    {
        public Conflict<AddUserConflict> GetExamples()
        {
            return FakeAddUserConflict.GetFake();
        }
    }
}