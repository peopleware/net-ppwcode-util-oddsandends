#region Using

using System;

using PPWCode.Util.OddsAndEnds.I.Extensions;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.DateTimeProvider
{
    public class LambdaDateTimeProvider : DateTimeProvider
    {
        /// <summary>
        /// LambdaDateTimeProvider x = new LambdaDateTimeProvider();
        /// x.LambdaToday = () => return DateTime.Today
        /// x.LambdaNow = () => return DateTime.Now
        /// DateTimeProvider.Current = x;
        /// </summary>

        public Func<DateTime> LambdaNow { get; set; }

        #region Overrides of DateTimeProvider

        public override DateTime Today
        {
            get { return LambdaNow().StripHours(); }
        }

        public override DateTime Now
        {
            get { return LambdaNow(); }
        }

        #endregion
    }
}