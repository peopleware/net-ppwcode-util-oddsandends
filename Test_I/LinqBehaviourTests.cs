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