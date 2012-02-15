using System;

namespace PPWCode.Util.OddsAndEnds.I.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime Today { get; }
        DateTime Now { get; }
    }
}