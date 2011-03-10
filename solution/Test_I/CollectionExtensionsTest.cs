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
    public class CollectionExtensionsTest
    {
        [TestMethod, Description("CollectionExtensions Difference behaviour Aggregate and Sum")]
        public void TestSumAggregate()
        {
            List<int?> l = new List<int?> { 0, 3, null, 4, 5 };
            int? sum = l.Sum();
            int? agg = l.Aggregate((int?)0, (s, x) => s + x);
            Assert.IsFalse(sum == (int?)null);
            Assert.IsTrue(agg == (int?)null);
        }

        [TestMethod, Description("CollectionExtensions NullableSum 1")]
        public void TestNullableSum1()
        {
            List<int?> l = new List<int?> { 0, 3, null, 4, 5 };
            Assert.AreEqual(null, l.NullableSum());
        }

        [TestMethod, Description("CollectionExtensions NullableSum 2")]
        public void TestNullableSum2()
        {
            List<int?> l = new List<int?> { 0, 3, 3, 4, 5 };
            Assert.AreEqual(15, l.NullableSum());
        }

        [TestMethod, Description("CollectionExtensions SetEqual")]
        public void TestSetEqual()
        {
            List<int> l1 = new List<int> { 1, 2, 3, 4 };
            List<int> l2 = new List<int> { 4, 3, 2, 1 };
            Assert.IsTrue(l1.SetEqual(l2));
        }

        [TestMethod, Description("CollectionExtensions IsNullOrEmpty #1")]
        public void TestIsNullOrEmpty1()
        {
            List<int> l1 = new List<int> { 1, 2, 3, 4 };
            Assert.IsFalse(l1.IsNullOrEmpty());
        }

        [TestMethod, Description("CollectionExtensions IsNullOrEmpty #2")]
        public void TestIsNullOrEmpty2()
        {
            List<int> l1 = new List<int>();
            Assert.IsTrue(l1.IsNullOrEmpty());
        }

        [TestMethod, Description("CollectionExtensions IsNullOrEmpty #3")]
        public void TestIsNullOrEmpty3()
        {
            List<int> l1 = null;
            Assert.IsTrue(l1.IsNullOrEmpty());
        }

        [TestMethod, Description("CollectionExtensions IsEmpty #1")]
        public void TestIsEmpty1()
        {
            List<int> l1 = new List<int> { 1, 2, 3, 4 };
            Assert.IsFalse(l1.IsEmpty());
        }

        [TestMethod, Description("CollectionExtensions IsEmpty #2")]
        public void TestIsEmpty2()
        {
            List<int> l1 = new List<int>();
            Assert.IsTrue(l1.IsEmpty());
        }

        [TestMethod, Description("CollectionExtensions IsEmpty #3")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIsEmpty3()
        {
            List<int> l1 = null;
            l1.IsEmpty();
        }
    }
}
