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

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.Extensions;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestClass]
    public class DateTimeExtensionsTest
    {
        [TestMethod, Description("DateTimeExtensions FirstDayOfCurrentQuarter")]
        public void TestFirstDayOfCurrentQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), new DateTime(2000, 3, 28).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), new DateTime(2000, 12, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 7, 1), new DateTime(2000, 7, 1).FirstDayOfQuarter());
        }

        [TestMethod, Description("DateTimeExtensions FirstDayOfNextQuarter")]
        public void TestFirstDayOfNextQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 4, 1), new DateTime(2000, 3, 28).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2001, 1, 1), new DateTime(2000, 12, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), new DateTime(2000, 7, 1).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 1, 1), new DateTime(2010, 10, 1).FirstDayOfNextQuarter());
        }

        [TestMethod, Description("DateTimeExtensions LastDayOfCurrentQuarter")]
        public void TestLastDayOfCurrentQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 3, 31), new DateTime(2000, 3, 28).LastDayOfCurrentQuarter());
            Assert.AreEqual(new DateTime(2000, 12, 31), new DateTime(2000, 12, 31).LastDayOfCurrentQuarter());
            Assert.AreEqual(new DateTime(2000, 9, 30), new DateTime(2000, 7, 1).LastDayOfCurrentQuarter());
        }

        [TestMethod, Description("DateTimeExtensions LastDayOfNextQuarter")]
        public void TestLastDayOfNextQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 6, 30), new DateTime(2000, 3, 28).LastDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2001, 3, 31), new DateTime(2000, 12, 31).LastDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2000, 12, 31), new DateTime(2000, 7, 1).LastDayOfNextQuarter());
        }

        [TestMethod, Description("DateTimeExtensions FirstDayOfQuarter(string)")]
        public void TestFirstDayOfQuarterString()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), "20001".FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2001, 4, 1), "20012".FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), "20004".FirstDayOfQuarter());
        }

        [TestMethod, Description("DateTimeExtensions FirstDayOfQuarter(int)")]
        public void TestFirstDayOfQuarterInt()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), 20001.FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2001, 4, 1), 20012.FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), 20004.FirstDayOfQuarter());
        }
    }
}
