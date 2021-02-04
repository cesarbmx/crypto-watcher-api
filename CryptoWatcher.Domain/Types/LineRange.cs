using System;


namespace CryptoWatcher.Domain.Types
{
    [Flags]
    public enum LineRange
    {
        THREE_HOURS = 1,
        ONE_DAY = 2,
        THREE_DAYS = 4,
        EIGHT_DAYS = 8,
        ONE_YEAR = 16
    }
}
