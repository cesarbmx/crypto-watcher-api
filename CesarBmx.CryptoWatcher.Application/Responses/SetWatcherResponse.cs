using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Requests;


namespace CesarBmx.CryptoWatcher.Application.Responses
{
    public class SetWatcherResponse:Response<SetWatcher, Watcher,SetWatcherConflictReason>
    {
        public SetWatcherResponse(SetWatcher request, Watcher response, SetWatcherConflictReason status) : base(request, response, status)
        {
        }
    }
}
