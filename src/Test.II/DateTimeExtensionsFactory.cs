using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
   public class DateTimeExtensionsFactory
    {
       public IEnumerable StripMilliseconds
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 10, 5, 5, 5, 5, 5)).Returns(new DateTime(2015, 10, 5, 5, 5, 5));
               yield return new TestCaseData(new DateTime(2015, 10, 5, 5, 5, 5, 1)).Returns(new DateTime(2015, 10, 5, 5, 5, 5));
               yield return new TestCaseData(new DateTime(2015, 10, 5, 5, 5, 5, 59)).Returns(new DateTime(2015, 10, 5, 5, 5, 5));
           }
       }

       public IEnumerable StripSeconds
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 05, 01, 7, 56, 59)).Returns(new DateTime(2015, 05, 01, 7, 56, 0));
               yield return new TestCaseData(new DateTime(2015, 05, 01, 7, 56, 1)).Returns(new DateTime(2015, 05, 01, 7, 56, 0));
               yield return new TestCaseData(new DateTime(2015, 05, 01, 7, 56, 0)).Returns(new DateTime(2015, 05, 01, 7, 56, 0));
               yield return new TestCaseData(new DateTime(2015, 05, 01, 7, 56, 0, 5)).Returns(new DateTime(2015, 05, 01, 7, 56, 0));
           }
       }

       public IEnumerable StripHours
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 06, 01, 20, 0, 0)).Returns(new DateTime(2015, 06, 01));
               yield return new TestCaseData(new DateTime(2015, 06, 01, 1, 0, 0)).Returns(new DateTime(2015, 06, 01));
               yield return new TestCaseData(new DateTime(2015, 06, 01, 23, 59, 59)).Returns(new DateTime(2015, 06, 01));
               yield return new TestCaseData(new DateTime(2015, 06, 01, 20, 0, 1)).Returns(new DateTime(2015, 06, 01));
               yield return new TestCaseData(new DateTime(2015, 06, 01, 20, 1, 0)).Returns(new DateTime(2015, 06, 01));
           }
       }

       public IEnumerable IsLegalSqlDate
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 06, 01)).Returns(true);
               yield return new TestCaseData(new DateTime(1753, 01, 01)).Returns(true);
               yield return new TestCaseData(new DateTime(1752, 12, 31)).Returns(false);
               yield return new TestCaseData(new DateTime(9999, 12, 31)).Returns(true);
           }
       }

       public IEnumerable IsLegalSqlDateNullableDatetime
       {
           get { yield return new TestCaseData((DateTime?)null).Returns(true); }
       }
    }
}
