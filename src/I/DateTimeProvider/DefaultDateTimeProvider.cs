#region Using

using System;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.DateTimeProvider
{
    public class DefaultDateTimeProvider : DateTimeProvider
    {
        #region Overrides of DateTimeProvider

        public override DateTime Today
        {
            get { return DateTime.Today; }
        }

        public override DateTime Now
        {
            get { return DateTime.Now; }
        }

        #endregion
    }
}