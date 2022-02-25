using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.CryptoWatcher.Application.Responses;

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
