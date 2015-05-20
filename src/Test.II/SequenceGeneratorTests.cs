using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.UnitTestHelpers;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
    [TestFixture]
    public class SequenceGeneratorTests
    {
        [Test, TestCaseSource(typeof(SequenceGeneratorFactory), "CreateRandomDecimalSequence")]
        public void CreateRandomDecimalSequence(int numberOfItems, double start, double end)
        {
            decimal sum = 0;
            decimal decimalStart = (decimal)start;
            decimal deDecimalEnd = (decimal)end;
            IEnumerable<decimal> decimals = SequenceGenerator.CreateRandomDecimalSequence(numberOfItems, start, end, ref sum);
            foreach (var oneDecimal in decimals)
            {
                Assert.IsTrue(decimalStart <= oneDecimal);
                Assert.IsTrue(deDecimalEnd >= oneDecimal);
            }
        }
    }
}
