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

using System.Globalization;
using System.Threading;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.Extensions;
using PPWCode.Util.OddsAndEnds.II.TypeConverter;

namespace PPWCode.Util.OddsAndEnds.II.Tests
{
    internal enum TestEnum
    {
        NONE,
        ONE,
        TWO
    }

    [TestFixture]
    public class EnumTests
    {
        [Test]
        public void TestMethod1()
        {
            const TestEnum E = TestEnum.ONE;
            Assert.AreEqual(@"ONE", E.ToString());
        }

        [Test]
        public void TestMethod2()
        {
            TestEnum? e = null;
            // ReSharper disable ExpressionIsAlwaysNull
            Assert.AreEqual(string.Empty, e.GetLocalizedDescription());
            // ReSharper restore ExpressionIsAlwaysNull
            e = TestEnum.ONE;
            Assert.AreEqual(@"ONE", e.ToString());
        }

        [Test]
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

        [Test]
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