using CesarBmx.CryptoWatcher.Application.ConflictReasons;
using CesarBmx.CryptoWatcher.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.CryptoWatcher.Application.FakeResponses
{
    public static class FakeAddIndicatorConflict
    {
        public static Conflict<AddIndicatorConflictReason> GetFake()
        {
            return new Conflict<AddIndicatorConflictReason>(AddIndicatorConflictReason.INDICATOR_ALREADY_EXISTS, IndicatorMessage.IndicatorWithSameIdAlreadyExists);
        }
    }
}
