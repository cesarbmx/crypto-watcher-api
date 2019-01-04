using CryptoWatcher.Application.FakeResponses;
using Swashbuckle.AspNetCore.Filters;

namespace CryptoWatcher.Api.ResponseExamples
{
    public class ScriptVariableListExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return ScriptVariableFakeResponse.GetFake_List();
        }
    }
}