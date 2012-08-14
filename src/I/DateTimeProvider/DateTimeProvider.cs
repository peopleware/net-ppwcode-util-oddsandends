#region Using

using System;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.DateTimeProvider
{
    public abstract class DateTimeProvider
    {
        private static DateTimeProvider s_Current = new DefaultDateTimeProvider();

        /// <summary>
        /// DateTime.Today => DateTimeProvider.Current.Today
        /// 
        /// </summary>
        public static DateTimeProvider Current
        {
            get { return s_Current; }
            set { s_Current = value; }
        }

        public abstract DateTime Today { get; }
        public abstract DateTime Now { get; }
    }
}