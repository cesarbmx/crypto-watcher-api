using System;


namespace CesarBmx.CryptoWatcher.Domain.Types
{
    [Flags]
    public enum Period
    {
        ONE_MINUTE = 1,
        FIVE_MINUTES = 2,
        FIFTEEN_MINUTES = 4,
        ONE_HOUR = 8,
        ONE_DAY = 16
    }
}
