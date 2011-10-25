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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.Extensions;

#endregion

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestClass]
    public class MathExtensionTests
    {
        [TestMethod]
        public void YearlyInterestRate1()
        {
            const double YearlyInterestRateAsPercentage = 3.25d;
            const double Result = 0.0325d;
            Assert.AreEqual(YearlyInterestRateAsPercentage.YearInterestFraction(), Result);
        }

        [TestMethod]
        public void YearlyInterestRate2()
        {
            const decimal YearlyInterestRateAsPercentage = 3.25m;
            const decimal Result = 0.0325m;
            Assert.AreEqual(YearlyInterestRateAsPercentage.YearInterestFraction(), Result);
        }

        [TestMethod]
        public void DailyInterestRate1()
        {
            const double YearlyInterestRateAsPercentage = 3.25d;
            const double Result = 0.00201740223579982992644387054037d;
            Assert.AreEqual(YearlyInterestRateAsPercentage.DayInterestFraction(new DateTime(2010, 11, 8), 23), Result, 1e-15d);
        }

        //[TestMethod]
        //public void DailyInterestRate2()
        //{
        //    const decimal YearlyInterestRateAsPercentage = 3.25m;
        //    const decimal Result = 0.002017402235799m;
        //    Assert.AreEqual(YearlyInterestRateAsPercentage.DayInterestFraction(new DateTime(2010, 11, 8), 23), Result);
        //}

        [TestMethod]
        public void MonthlyInterestRate1()
        {
            const double YearlyInterestRateAsPercentage = 3.25d;
            const double Result = 0.01071804639746965373238059839476d;
            Assert.AreEqual(YearlyInterestRateAsPercentage.MonthInterestFraction(4), Result, 1e-15d);
        }

        //[TestMethod]
        //public void MonthlyInterestRate2()
        //{
        //    const decimal YearlyInterestRateAsPercentage = 3.25m;
        //    const decimal Result = 0.0107180463974696m;
        //    Assert.AreEqual(YearlyInterestRateAsPercentage.MonthInterestFraction(4), Math.Round(Result, 15));
        //}

        [TestMethod]
        public void QuartlyInterestRate1()
        {
            const double YearlyInterestRateAsPercentage = 3.25d;
            const double Result = 0.02427729349700212108269485941176d;
            Assert.AreEqual(YearlyInterestRateAsPercentage.QuarterInterestFraction(3), Result, 1e-15d);
        }

        //[TestMethod]
        //public void QuartlyInterestRate2()
        //{
        //    const decimal YearlyInterestRateAsPercentage = 3.25m;
        //    const decimal Result = 0.0242772934970021m;
        //    Assert.AreEqual(YearlyInterestRateAsPercentage.QuarterInterestFraction(3), Math.Round(Result, 15));
        //}
    }
}
