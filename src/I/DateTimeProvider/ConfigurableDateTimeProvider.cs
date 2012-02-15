#region Using

using System;

using PPWCode.Util.OddsAndEnds.I.Extensions;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.DateTimeProvider
{
    public class ConfigurableDateTimeProvider : IDateTimeProvider
    {
        public ConfigurableDateTimeProvider() : this(DateTime.Now)
        {
        }

        public ConfigurableDateTimeProvider(DateTime now)
        {
            m_Today = now.StripHours();
            m_Now = now;
        }

        public static IDateTimeProvider CreateInstance()
        {
            return new ConfigurableDateTimeProvider();
        }


        private DateTime m_Today;

        public DateTime Today
        {
            get { return m_Today; }
            set { m_Today = value.StripHours(); }
        }

        private DateTime m_Now;

        public DateTime Now
        {
            get { return m_Now; }
            set { m_Now = value; }
        }
    }
}