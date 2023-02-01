using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddIndicatorConflict
    {
        public static Conflict<AddIndicatorConflict> GetFake()
        {
            return new Conflict<AddIndicatorConflict>(AddIndicatorConflict.INDICATOR_ALREADY_EXISTS, IndicatorMessage.IndicatorWithSameIdAlreadyExists);
        }
    }
}
