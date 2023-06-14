using CesarBmx.CryptoWatcher.Application.Conflicts;
using CesarBmx.CryptoWatcher.Application.Messages;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddIndicatorConflict
    {
        public static AddIndicatorConflict GetFake()
        {
            return new AddIndicatorConflict(AddIndicatorConflictReason.INDICATOR_ALREADY_EXISTS, IndicatorMessage.IndicatorWithSameIdAlreadyExists);
        }
    }
}
