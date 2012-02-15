#region Using

using System;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.DateTimeProvider
{
    public class GenuineDateTimeProvider : IDateTimeProvider
    {
        public GenuineDateTimeProvider()
        {
        }

        public static IDateTimeProvider CreateInstance()
        {
            return new GenuineDateTimeProvider();
        }

        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}