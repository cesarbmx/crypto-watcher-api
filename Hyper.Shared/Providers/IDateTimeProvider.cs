using System;


namespace Hyper.Shared.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetDateFromHeader();
    }
}