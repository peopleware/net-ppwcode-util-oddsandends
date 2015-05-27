// Copyright 2014 by PeopleWare n.v..
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.Extensions;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
    [TestFixture]
    public class DateTimeExtensionsTest
    {
        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "StripMilliseconds")]
        public DateTime StripMillisecondsTest(DateTime dateTime)
        {
            return dateTime.StripMilliseconds();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "StripSeconds")]
        public DateTime StripSecondsTest(DateTime dateTime)
        {
            return dateTime.StripSeconds();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "StripHours")]
        public DateTime StripHours(DateTime dateTime)
        {
            return dateTime.StripHours();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "IsLegalSqlDate")]
        public bool IsLegalSqlDateTest(DateTime dateTime)
        {
            return dateTime.IsLegalSqlDate();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "IsLegalSqlDateNullableDatetime")]
        public bool IsLegalSqlDateNullableDatetimeTest(DateTime? dateTime)
        {
            return dateTime.IsLegalSqlDate();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "AddQuarters")]
        public DateTime AddQuartersTest(DateTime dateTime, int quarters)
        {
            return dateTime.AddQuarters(quarters);
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "StripMillisecondsFromNullableDatetime")]
        public DateTime? StripMillisecondsForNullableDatetimeTest(DateTime? dateTime)
        {
            return dateTime.StripMilliseconds();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "StripSecondsFromNullableDatetime")]
        public DateTime? StripSecondsFromNullableDatetimeTest(DateTime? dateTime)
        {
            return dateTime.StripSeconds();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "StripHoursFromNullableDatetime")]
        public DateTime? StripHoursFromNullableDatetimeTest(DateTime? dateTime)
        {
            return dateTime.StripHours();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "AddMonthsForNullableDatetime")]
        public DateTime? AddMonthsForNullableDatetimeTest(DateTime? dateTime, int numberOfMonths)
        {
            return dateTime.AddMonths(numberOfMonths);
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "AddQuartersForNullableDatetime")]
        public DateTime? AddQuartersForNullableDatetimeTest(DateTime? dateTime, int quarters)
        {
            return dateTime.AddQuarters(quarters);
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "ImmediateFirstOfQuarter")]
        public DateTime ImmediateFirstOfQuarterTest(DateTime dateTime)
        {
            return dateTime.ImmediateFirstOfQuarter();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "IsFirstDayOfQuarter")]
        public bool IsFirstDayOfQuarterTest(DateTime dateTime)
        {
            return dateTime.IsFirstDayOfQuarter();
        }

        [Test, Description("DateTimeExtensions FirstDayOfCurrentQuarter")]
        public void TestFirstDayOfCurrentQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), new DateTime(2000, 3, 28).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), new DateTime(2000, 12, 31).FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 7, 1), new DateTime(2000, 7, 1).FirstDayOfQuarter());
        }

        [Test, Description("DateTimeExtensions FirstDayOfNextQuarter")]
        public void TestFirstDayOfNextQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 4, 1), new DateTime(2000, 3, 28).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2001, 1, 1), new DateTime(2000, 12, 31).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), new DateTime(2000, 7, 1).FirstDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2011, 1, 1), new DateTime(2010, 10, 1).FirstDayOfNextQuarter());
        }

        [Test, Description("DateTimeExtensions LastDayOfCurrentQuarter")]
        public void TestLastDayOfCurrentQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 3, 31), new DateTime(2000, 3, 28).LastDayOfCurrentQuarter());
            Assert.AreEqual(new DateTime(2000, 12, 31), new DateTime(2000, 12, 31).LastDayOfCurrentQuarter());
            Assert.AreEqual(new DateTime(2000, 9, 30), new DateTime(2000, 7, 1).LastDayOfCurrentQuarter());
        }

        [Test, Description("DateTimeExtensions LastDayOfNextQuarter")]
        public void TestLastDayOfNextQuarter()
        {
            Assert.AreEqual(new DateTime(2000, 6, 30), new DateTime(2000, 3, 28).LastDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2001, 3, 31), new DateTime(2000, 12, 31).LastDayOfNextQuarter());
            Assert.AreEqual(new DateTime(2000, 12, 31), new DateTime(2000, 7, 1).LastDayOfNextQuarter());
        }

        [Test, Description("DateTimeExtensions FirstDayOfQuarter(string)")]
        public void TestFirstDayOfQuarterString()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), "20001".FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2001, 4, 1), "20012".FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), "20004".FirstDayOfQuarter());
        }

        [Test, Description("DateTimeExtensions FirstDayOfQuarter(int)")]
        public void TestFirstDayOfQuarterInt()
        {
            Assert.AreEqual(new DateTime(2000, 1, 1), 20001.FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2001, 4, 1), 20012.FirstDayOfQuarter());
            Assert.AreEqual(new DateTime(2000, 10, 1), 20004.FirstDayOfQuarter());
        }

        [Test, Description("DateTimeExtensions FirstDayOfPreviousQuarter")]
        public void TestFirstDayOfPreviousQuarter()
        {
            Assert.AreEqual(new DateTime(2013, 10, 1), new DateTime(2014, 2, 28).FirstDayOfPreviousQuarter());
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "LastDayOfCurrentQuarter")]
        public DateTime LastDayOfCurrentQuarterTest(DateTime dateTime)
        {
            return dateTime.LastDayOfCurrentQuarter();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "LastDayOfNextQuarter")]
        public DateTime LastDayOfNextQuarterTest(DateTime dateTime)
        {
            return dateTime.LastDayOfNextQuarter();
        }

        [Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "IsFirstDayOfMonth")]
        public bool IsFirstDayOfMonthTest(DateTime dateTime)
        {
            return dateTime.IsFirstDayOfMonth();
        }

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "IsFirstDayOfMonthForNullableDatetime")]
        //public bool? IsFirstDayOfMonthForNullableDatetimeTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "ImmediateFirstOfMonth")]
        //public DateTime ImmediateFirstOfMonthTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "FirstDayOfNextMonth")]
        //public DateTime FirstDayOfNextMonthTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "DaysBetween")]
        //public int DaysBetweenTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "MonthBetween")]
        //public int MonthBetweenTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "QuartersBetween")]
        //public int QuartersBetweenTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "ImmediateFirstOfYear")]
        //public DateTime ImmediateFirstOfYearTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "FirstDayOfNextYear")]
        //public DateTime FirstDayOfNextYearTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "FirstDayOfMonth")]
        //public DateTime FirstDayOfMonthTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "LastDayOfMonth")]
        //public DateTime LastDayOfMonthTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "NumberOfDaysInYear")]
        //public int NumberOfDaysInYearTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}

        //[Test, TestCaseSource(typeof(DateTimeExtensionsFactory), "AgeInYears")]
        //public int AgeInYearsTest()
        //{
        //    // todo add tests
        //    throw new NotImplementedException();
        //}
    }
}