using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
  public class ValidationFactory
    {
        public static IEnumerable IsValidDmfaNumber
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(null);
                yield return new TestCaseData("jjjjjj").Returns(null);
                yield return new TestCaseData("1111").Returns(null);
            }
        }

        public static IEnumerable ValidRrn
        {
            get { yield return new TestCaseData(string.Empty).Returns(false); }
        }

        public static IEnumerable StrictValidRrn
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
            }
        }

        public static IEnumerable PadRrn
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
            }
        }

        public static IEnumerable GetBirthDateFromRrn
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(null);
            }
        }

        public static IEnumerable GetDigitStream
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
            }
        }

        public static IEnumerable ValidRsz
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
            }
        }

        public static IEnumerable StrictValidRsz
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
            }
        }

        public static IEnumerable PadRsz
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
            }
        }

        public static IEnumerable ValidKbo
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
            }
        }

        public static IEnumerable StrictValidKbo
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
            }
        }

        public static IEnumerable PadKbo
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
            }
        }

        public static IEnumerable ValidVat
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
            }
        }

        public static IEnumerable StrictValidVat
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
            }
        }
    }
}
