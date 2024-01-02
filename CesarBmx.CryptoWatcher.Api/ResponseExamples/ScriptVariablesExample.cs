using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    public class ScriptVariableListExample : IExamplesProvider<ScriptVariablesResponse>
    {
        public ScriptVariablesResponse GetExamples()
        {
            return FakeScriptVariables.GetFake_List();
        }
    }
}