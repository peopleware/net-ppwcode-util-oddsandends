using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
