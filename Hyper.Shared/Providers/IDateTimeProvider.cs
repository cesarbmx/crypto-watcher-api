using System;


namespace CryptoWatcher.Shared.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetDateFromHeader();
    }
}