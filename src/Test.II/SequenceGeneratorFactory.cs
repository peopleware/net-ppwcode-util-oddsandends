using System.Collections;

using NUnit.Framework;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
    public class SequenceGeneratorFactory
    {
        public static IEnumerable CreateRandomDecimalSequence
        {
            get
            {
                yield return new TestCaseData(3, 1.25, 5.68);
                yield return new TestCaseData(0, 1.2, 1.6);
                yield return new TestCaseData(853, -1, 1);
                yield return new TestCaseData(952, -1, 0);
                yield return new TestCaseData(5, 0, 1);
                yield return new TestCaseData(100, double.MaxValue - 1, double.MaxValue);
            }
        }
    }
}
