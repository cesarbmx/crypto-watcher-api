using CryptoWatcher.Application.FakeResponses;
using CryptoWatcher.Application.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class ScriptVariableListExample : IExamplesProvider<ScriptVariableSetResponse>
    {
        public ScriptVariableSetResponse GetExamples()
        {
            return ScriptVariableSetFakeResponse.GetFake_List();
        }
    }
}