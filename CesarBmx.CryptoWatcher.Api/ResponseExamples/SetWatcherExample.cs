using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.FakeResponses;
using CesarBmx.CryptoWatcher.Application.Responses;
using CesarBmx.Shared.Api.ResponseExamples;
using System.ComponentModel;

namespace CesarBmx.CryptoWatcher.Api.ResponseExamples
{
    [DisplayName("SetWatcherResponse")]
    public class SetWatcherExample : ResultExample<WatcherResponse, SetWatcherError, WatcherResponseExample> //IExamplesProvider<Result<WatcherResponse, SetWatcherErrors>
    {

    }
}