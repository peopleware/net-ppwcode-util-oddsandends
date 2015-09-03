using System;
using System.Collections;

using NUnit.Framework;

namespace PPWCode.Util.OddsAndEnds.II.Tests
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
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("111111111111").Returns(false);
                yield return new TestCaseData("72020290080").Returns(false);

                // controle geboortedatum voor 01/01/2000
                yield return new TestCaseData("67 06 29 389 93").Returns(true);
                yield return new TestCaseData("67.06.29.389.93").Returns(true);
                yield return new TestCaseData("67062938993").Returns(true); 

                // controle geboortedatum na 01/01/2000
                yield return new TestCaseData("00 09 15 011 19  ").Returns(true);
                yield return new TestCaseData("00091501119").Returns(true);

                yield return new TestCaseData("00Q09IsZot15 011 19  ").Returns(true);
            }
        }

        public static IEnumerable StrictValidRrn
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("111111111111").Returns(false);
                yield return new TestCaseData("72020290080").Returns(false);
                yield return new TestCaseData("00Q09IsZot15 011 19  ").Returns(false);

                // controle geboortedatum voor 01/01/2000
                yield return new TestCaseData("67 06 29 389 93").Returns(false);
                yield return new TestCaseData("67.06.29.389.93").Returns(false);
                yield return new TestCaseData("67062938993").Returns(true);

                // controle geboortedatum na 01/01/2000
                yield return new TestCaseData("00 09 15 011 19  ").Returns(false);
                yield return new TestCaseData("00091501119").Returns(true);              
            }
        }

        public static IEnumerable PadRrn
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
                yield return new TestCaseData("11").Returns("00000000011");
            }
        }

        public static IEnumerable GetBirthDateFromRrn
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(null);

                // controle geboortedatum voor 01/01/2000
                yield return new TestCaseData("67.06.29.389.93").Returns(new DateTime(1967, 06, 29));
                yield return new TestCaseData("67062938993").Returns(new DateTime(1967, 06, 29));

                // controle geboortedatum na 01/01/2000
                yield return new TestCaseData("00 09 15 011 19  ").Returns(new DateTime(2000, 09, 15));
                yield return new TestCaseData("00091501119").Returns(new DateTime(2000, 09, 15));
            }
        }

        public static IEnumerable GetDigitStream
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
                yield return new TestCaseData("123").Returns("123");
                yield return new TestCaseData("a123").Returns("123");
                yield return new TestCaseData("1a23").Returns("123");
                yield return new TestCaseData("123a").Returns("123");
                yield return new TestCaseData(" 123").Returns("123");
                yield return new TestCaseData("a1jjjj23").Returns("123");
                yield return new TestCaseData("123ddd").Returns("123");
                yield return new TestCaseData("ddd1////23?,.^$#").Returns("123");
            }
        }

        public static IEnumerable ValidRsz
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("010010108").Returns(true);
                yield return new TestCaseData("0.100.101 08").Returns(true);
                yield return new TestCaseData("010010109").Returns(false);
                yield return new TestCaseData("10010108").Returns(true);
                yield return new TestCaseData("951068994").Returns(true);
                yield return new TestCaseData("951068984").Returns(false);
                yield return new TestCaseData("5109839040").Returns(true);
                yield return new TestCaseData("5109839041").Returns(false);
            }
        }

        public static IEnumerable StrictValidRsz
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("010010108").Returns(false);
                yield return new TestCaseData("0.100.101 08").Returns(false);
                yield return new TestCaseData("010010109").Returns(false);
                yield return new TestCaseData("10010108").Returns(false);
                yield return new TestCaseData("951068994").Returns(false);
                yield return new TestCaseData("951068984").Returns(false);
                yield return new TestCaseData("5109839040").Returns(true);
                yield return new TestCaseData("5109839041").Returns(false);
            }
        }

        public static IEnumerable PadRsz
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
                yield return new TestCaseData("483").Returns("0000000483");
            }
        }

        public static IEnumerable ValidKbo
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("896073528").Returns(true);
                yield return new TestCaseData("8.9607.3528").Returns(true);
                yield return new TestCaseData("8 9607 3528").Returns(true);
                yield return new TestCaseData("2154199061").Returns(true);
                yield return new TestCaseData("0899754677").Returns(true);
                yield return new TestCaseData("0000000000").Returns(false);
                yield return new TestCaseData("0204908936").Returns(true);
                yield return new TestCaseData("0204908937").Returns(false);
                yield return new TestCaseData("0204908947").Returns(false);
            }
        }

        public static IEnumerable StrictValidKbo
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("896073528").Returns(false);
                yield return new TestCaseData("8.9607.3528").Returns(false);
                yield return new TestCaseData("8 9607 3528").Returns(false);
                yield return new TestCaseData("2154199061").Returns(true);
                yield return new TestCaseData("0899754677").Returns(true);
                yield return new TestCaseData("0000000000").Returns(false);
                yield return new TestCaseData("0204908936").Returns(true);
                yield return new TestCaseData("0204908937").Returns(false);
                yield return new TestCaseData("0204908947").Returns(false);
            }
        }

        public static IEnumerable PadKbo
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(string.Empty);
                yield return new TestCaseData("1").Returns("0000000001");
            }
        }

        public static IEnumerable ValidVat
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("BE 896415503").Returns(true);
                yield return new TestCaseData("899997573").Returns(true);
                yield return new TestCaseData("899842571").Returns(true);
                yield return new TestCaseData("899475951").Returns(true);
                yield return new TestCaseData("000000000").Returns(false);
                yield return new TestCaseData("400 833 296").Returns(true);
                yield return new TestCaseData("400000680").Returns(true);
            }
        }

        public static IEnumerable StrictValidVat
        {
            get
            {
                yield return new TestCaseData(string.Empty).Returns(false);
                yield return new TestCaseData("400 833 296").Returns(false);
                yield return new TestCaseData("400000680").Returns(true);
                yield return new TestCaseData("899997573").Returns(true);
            }
        }
    }
}
