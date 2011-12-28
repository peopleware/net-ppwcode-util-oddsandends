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

using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestClass]
    public class LinqBehaviourTests
    {
        [TestMethod]
        public void SumTests1()
        {
            IEnumerable<int?> items = new int?[]
            {
                1, 2, 3
            };
            int? result = items.Sum();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void SumTests2()
        {
            IEnumerable<int?> items = new int?[]
            {
                1, null, 3
            };
            int? result = items.Sum();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void SumTests3()
        {
            IEnumerable<int?> items = new int?[]
            {
                null, null, null
            };
            int? result = items.Sum();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void SumTests4()
        {
            IEnumerable<int?> items = Enumerable.Empty<int?>();
            int? result = items.Sum();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void SumTests5()
        {
            IEnumerable<int> items = Enumerable.Empty<int>();
            int result = items.Sum();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AllTests1()
        {
            IEnumerable<int> items = Enumerable.Empty<int>();
            bool result = items.All(o => o > 0);
            Assert.IsTrue(result);
        }
    }
}