//Copyright 2004 - $Date: 2008-11-15 23:58:07 +0100 (za, 15 nov 2008) $ by PeopleWare n.v..

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

#region Using

using System;

using PPWCode.Util.OddsAndEnds.II.Extensions;

#endregion

namespace PPWCode.Util.OddsAndEnds.II.DateTimeProvider
{
    /// <summary>
    /// LambdaDateTimeProvider is a DateTimeProvider that
    /// is configured by passing a lambda function in the
    /// constructor that returns a DateTime representing
    /// Now.  For Today the DateTimeProvider returns the day
    /// of the Now moment.
    /// </summary>
    /// <example>
    /// LambdaDateTimeProvider x = new LambdaDateTimeProvider(() => DateTime.Now);
    /// DateTimeProvider.Current = x;
    /// </example>
    public class LambdaDateTimeProvider : DateTimeProvider
    {
        public LambdaDateTimeProvider(Func<DateTime> lambdaNow)
        {
            m_LambdaNow = lambdaNow;
            m_LambdaToday = () => m_LambdaNow().StripHours();
        }

        public LambdaDateTimeProvider(Func<DateTime> lambdaNow, Func<DateTime> lambdaToday)
        {
            m_LambdaNow = lambdaNow;
            m_LambdaToday = lambdaToday;
        }

        private readonly Func<DateTime> m_LambdaNow;
        private readonly Func<DateTime> m_LambdaToday;

        public override DateTime Now
        {
            get { return m_LambdaNow(); }
        }

        public override DateTime Today
        {
            get { return m_LambdaToday(); }
        }
    }
}