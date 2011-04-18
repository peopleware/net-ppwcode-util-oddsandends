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
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.Extensions;

#endregion

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestClass]
    public class DateTimeHelperTest
    {
        private class DateInterval
        {
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        [TestMethod, Description("DateTimeHelper IsSequenceConsecutive")]
        public void TestIsSequenceConsecutive()
        {
            Func<object, DateTime?> extractStartDate = o => ((DateInterval)o).StartDate;
            Func<object, DateTime?> extractEndDate = o => ((DateInterval)o).EndDate;

            Assert.IsTrue(((IEnumerable<object>)null).IsConsecutiveSequence(extractStartDate, extractEndDate));

            IEnumerable<object> lst = Enumerable.Empty<object>();
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = null,
                    EndDate = null
                },
            };
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today,
                    EndDate = null
                },
            };
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = null,
                    EndDate = DateTime.Today
                },
            };
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(1),
                    EndDate = DateTime.Today
                },
            };
            Assert.IsFalse(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today
                },
            };
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today
                },
                new DateInterval
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today
                },
            };
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today
                },
                new DateInterval
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                },
            };
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today
                },
                new DateInterval
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(-1)
                },
            };
            Assert.IsFalse(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today
                },
                new DateInterval
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                },
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(1),
                    EndDate = DateTime.Today.AddDays(2)
                },
            };
            Assert.IsTrue(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));

            lst = new List<object>
            {
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today
                },
                new DateInterval
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                },
                new DateInterval
                {
                    StartDate = DateTime.Today.AddDays(1),
                    EndDate = DateTime.Today
                },
            };
            Assert.IsFalse(lst.IsConsecutiveSequence(extractStartDate, extractEndDate));
        }

        private static DateTime? DoTheTest1(DateTime? dt)
        {
            return dt;
        }

        [TestMethod]
        public void SomeTest1()
        {
            DateTime dt = DateTime.Now;
            DateTime? result = DoTheTest1(dt);
            Assert.AreEqual(dt, result);
        }

        private static DateTime DoTheTest2(DateTime? dt)
        {
            return dt.HasValue ? dt.Value : DateTime.Now;
        }

        [TestMethod]
        public void SomeTest21()
        {
            DateTime dt = DateTime.Now;
            DateTime result = DoTheTest2(dt);
            Assert.AreEqual(dt, result);
        }

        [TestMethod]
        public void SomeTest22()
        {
            DateTime? dt = null;
            DateTime result = DoTheTest2(dt);
            Assert.AreEqual(DateTime.Now, result);
        }
    }
}