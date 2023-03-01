using CesarBmx.Notification.Application.FakeResponses;
using CesarBmx.Notification.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.Notification.Api.ResponseExamples
{
    public class ScriptVariableListExample : IExamplesProvider<ScriptVariables>
    {
        public ScriptVariables GetExamples()
        {
            return FakeScriptVariables.GetFake_List();
        }
    }
}