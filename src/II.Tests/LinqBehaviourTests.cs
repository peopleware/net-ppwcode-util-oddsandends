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

using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace PPWCode.Util.OddsAndEnds.II.Tests
{
    [TestFixture]
    public class LinqBehaviourTests
    {
        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
        public void SumTests4()
        {
            IEnumerable<int?> items = Enumerable.Empty<int?>();
            int? result = items.Sum();
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void SumTests5()
        {
            IEnumerable<int> items = Enumerable.Empty<int>();
            int result = items.Sum();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void AllTests1()
        {
            IEnumerable<int> items = Enumerable.Empty<int>();
            bool result = items.All(o => o > 0);
            Assert.IsTrue(result);
        }
    }
}