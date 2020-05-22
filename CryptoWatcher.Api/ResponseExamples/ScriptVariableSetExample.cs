using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class ScriptVariableListExample : IExamplesProvider<ScriptVariableSet>
    {
        public ScriptVariableSet GetExamples()
        {
            return FakeScriptVariableSet.GetFake_List();
        }
    }
}