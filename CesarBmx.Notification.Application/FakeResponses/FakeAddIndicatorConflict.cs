using CesarBmx.Notification.Application.Conflicts;
using CesarBmx.Notification.Application.Messages;
using CesarBmx.Shared.Application.Responses;

namespace CesarBmx.Notification.Application.FakeResponses
{
    public static class FakeAddIndicatorConflict
    {
        public static Conflict<AddIndicatorConflict> GetFake()
        {
            return new Conflict<AddIndicatorConflict>(AddIndicatorConflict.INDICATOR_ALREADY_EXISTS, IndicatorMessage.IndicatorWithSameIdAlreadyExists);
        }
    }
}
