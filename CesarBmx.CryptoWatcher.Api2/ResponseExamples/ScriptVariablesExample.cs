using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api2.ResponseExamples
{
    public class ScriptVariableListExample : IExamplesProvider<ScriptVariables>
    {
        public ScriptVariables GetExamples()
        {
            return FakeScriptVariables.GetFake_List();
        }
    }
}