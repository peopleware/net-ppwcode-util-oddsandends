using System.Collections.Generic;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.UnitTestHelpers;

namespace PPWCode.Util.OddsAndEnds.II.Tests
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
