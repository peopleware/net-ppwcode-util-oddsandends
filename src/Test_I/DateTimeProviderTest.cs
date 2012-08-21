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

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.I.DateTimeProvider;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestFixture]
    public class DateTimeProviderTest
    {
        [TearDown]
        public void TearDown()
        {
            DateTimeProvider.Reset();
        }

        [Test, Description("Test normal time")]
        public void DateTimeProviderTest1()
        {
            DateTime now = DateTimeProvider.Current.Now;
            DateTime today = DateTimeProvider.Current.Today;

            Assert.IsTrue(new DateTime(2010, 1, 1) < now);
            Assert.IsTrue(new DateTime(2010, 1, 1) < today);
        }

        [Test, Description("Test fixed time")]
        public void DateTimeProviderTest2()
        {
            DateTimeProvider fixedTime = 
                new LambdaDateTimeProvider(() => new DateTime(2000,1,1,10,15,0));
            DateTimeProvider.Current = fixedTime;

            Assert.AreEqual(new DateTime(2000,1,1,10,15,0), DateTimeProvider.Current.Now);
            Assert.AreEqual(new DateTime(2000, 1, 1), DateTimeProvider.Current.Today);
        }

        [Test, Description("Test incremental time")]
        public void DateTimeProviderTest3()
        {
            int t = 0;
            Func<DateTime> lambda = () =>
            {
                t = t + 1;
                return (new DateTime(2000, 1, 1, 10, 0, 0)).AddHours(t);
            };
            DateTimeProvider.Current = new LambdaDateTimeProvider(lambda);

            Assert.AreEqual(new DateTime(2000, 1, 1, 11, 0, 0), DateTimeProvider.Current.Now);
            Assert.AreEqual(new DateTime(2000, 1, 1, 12, 0, 0), DateTimeProvider.Current.Now);
            Assert.AreEqual(new DateTime(2000, 1, 1, 13, 0, 0), DateTimeProvider.Current.Now);
            Assert.AreEqual(new DateTime(2000, 1, 1, 14, 0, 0), DateTimeProvider.Current.Now);
        }
    }
}
