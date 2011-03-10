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

using System.Globalization;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.Extensions;
using PPWCode.Util.OddsAndEnds.I.TypeConverter;

#endregion

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    internal enum TestEnum
    {
        NONE,
        ONE,
        TWO
    }

    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            const TestEnum E = TestEnum.ONE;
            Assert.AreEqual(@"ONE", E.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            TestEnum? e = null;
            Assert.AreEqual(string.Empty, e.GetLocalizedDescription());
            e = TestEnum.ONE;
            Assert.AreEqual(@"ONE", e.ToString());
        }

        [TestMethod]
        public void TestMethod3()
        {
            const TestEnum E = TestEnum.ONE;
            TypeConverterHelper.RegisterTypeConverter<TestEnum, LocalizedEnumConverter>();
            Thread.CurrentThread.CurrentCulture = new CultureInfo(@"en-US");
            Assert.AreEqual(@"ONE", E.GetLocalizedDescription());
            Thread.CurrentThread.CurrentCulture = new CultureInfo(@"nl-BE");
            Assert.AreEqual(@"Eén", E.GetLocalizedDescription());
            Thread.CurrentThread.CurrentCulture = new CultureInfo(@"nl-NL");
            Assert.AreEqual(@"Eén", E.GetLocalizedDescription());
        }

        [TestMethod]
        public void TestMethod4()
        {
            const TestEnum E = TestEnum.ONE;
            TypeConverterHelper.RegisterTypeConverter<TestEnum, LocalizedEnumConverter>();
            Assert.AreEqual(@"ONE", E.GetLocalizedDescription(new CultureInfo(@"en-US")));
            Assert.AreEqual(@"Eén", E.GetLocalizedDescription(new CultureInfo(@"nl-BE")));
            Assert.AreEqual(@"Eén", E.GetLocalizedDescription(new CultureInfo(@"nl-NL")));
        }
    }
}