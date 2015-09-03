using System;
using System.Collections;

using NUnit.Framework;

namespace PPWCode.Util.OddsAndEnds.II.Tests
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

       public IEnumerable AddQuarters
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 05, 01), 1).Returns(new DateTime(2015, 08, 01));
               yield return new TestCaseData(new DateTime(2015, 08, 01), -1).Returns(new DateTime(2015, 05, 01));
               yield return new TestCaseData(new DateTime(2015, 03, 31), 1).Returns(new DateTime(2015, 06, 30));
               yield return new TestCaseData(new DateTime(2015, 02, 28), 1).Returns(new DateTime(2015, 05, 28));
               yield return new TestCaseData(new DateTime(2014, 11, 30), 1).Returns(new DateTime(2015, 02, 28));
               yield return new TestCaseData(new DateTime(2014, 11, 29), 1).Returns(new DateTime(2015, 02, 28));
               yield return new TestCaseData(new DateTime(2014, 11, 28), 1).Returns(new DateTime(2015, 02, 28));
           }
       }

       public IEnumerable StripMillisecondsFromNullableDatetime
       {
           get
           {
              yield return new TestCaseData((DateTime?)null).Returns(null);
           }
       }

       public IEnumerable StripSecondsFromNullableDatetime
       {
           get { yield return new TestCaseData((DateTime?)null).Returns(null); }
       }

       public IEnumerable StripHoursFromNullableDatetime
       {
           get
           {
               yield return new TestCaseData((DateTime?)null).Returns(null);
           }
       }

       public IEnumerable AddMonthsForNullableDatetime
       {
           get
           {
               yield return new TestCaseData((DateTime?)null, 5).Returns(null);
           }
       }

       public IEnumerable AddQuartersForNullableDatetime
       {
           get
           {
               yield return new TestCaseData((DateTime?)null, 2).Returns(null);
           }
       }

       public IEnumerable ImmediateFirstOfQuarter
       {
           get { yield return new TestCaseData(new DateTime(2015, 01, 01)).Returns(new DateTime(2015, 01, 01)); }
       }

       public IEnumerable IsFirstDayOfQuarter
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 01, 01)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 04, 01)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 07, 01)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 10, 01)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 02, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 03, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 05, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 06, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 08, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 09, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 11, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 12, 01)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 01, 02)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 02, 02)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 03, 30)).Returns(false);
           }
       }

       public IEnumerable LastDayOfCurrentQuarter
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 01, 01)).Returns(new DateTime(2015, 03, 31));
               yield return new TestCaseData(new DateTime(2015, 02, 05)).Returns(new DateTime(2015, 03, 31));
               yield return new TestCaseData(new DateTime(2015, 03, 15)).Returns(new DateTime(2015, 03, 31));
               yield return new TestCaseData(new DateTime(2015, 06, 30)).Returns(new DateTime(2015, 06, 30));
               yield return new TestCaseData(new DateTime(2015, 04, 01)).Returns(new DateTime(2015, 06, 30));
               yield return new TestCaseData(new DateTime(2015, 05, 31)).Returns(new DateTime(2015, 06, 30));
           }
       }

       public IEnumerable LastDayOfNextQuarter
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 01, 01)).Returns(new DateTime(2015, 06, 30));
               yield return new TestCaseData(new DateTime(2015, 02, 05)).Returns(new DateTime(2015, 06, 30));
               yield return new TestCaseData(new DateTime(2015, 03, 15)).Returns(new DateTime(2015, 06, 30));
               yield return new TestCaseData(new DateTime(2015, 06, 30)).Returns(new DateTime(2015, 09, 30));
               yield return new TestCaseData(new DateTime(2015, 04, 01)).Returns(new DateTime(2015, 09, 30));
               yield return new TestCaseData(new DateTime(2015, 05, 31)).Returns(new DateTime(2015, 09, 30)); 
           }
       }

       public IEnumerable IsFirstDayOfMonth
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 1, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 2, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 3, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 4, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 5, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 6, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 7, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 8, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 9, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 10, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 11, 1)).Returns(true);
               yield return new TestCaseData(new DateTime(2015, 12, 1)).Returns(true);

               yield return new TestCaseData(new DateTime(2015, 1, 2)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 2, 3)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 3, 4)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 4, 5)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 5, 6)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 6, 7)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 7, 8)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 8, 9)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 9, 11)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 10, 20)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 11, 25)).Returns(false);
               yield return new TestCaseData(new DateTime(2015, 12, 31)).Returns(false);
           }
       }

       public IEnumerable IsFirstDayOfMonthForNullableDatetime
       {
           get { yield return new TestCaseData((DateTime?)null).Returns(null); }
       }

       public IEnumerable ImmediateFirstOfMonth
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 01, 01)).Returns(new DateTime(2015, 01, 01));
               yield return new TestCaseData(new DateTime(2015, 01, 02)).Returns(new DateTime(2015, 02, 01));
               yield return new TestCaseData(new DateTime(2015, 02, 01)).Returns(new DateTime(2015, 02, 01));
               yield return new TestCaseData(new DateTime(2015, 03, 01)).Returns(new DateTime(2015, 03, 01));
               yield return new TestCaseData(new DateTime(2015, 04, 01)).Returns(new DateTime(2015, 04, 01));
               yield return new TestCaseData(new DateTime(2015, 05, 01)).Returns(new DateTime(2015, 05, 01));
               yield return new TestCaseData(new DateTime(2015, 06, 01)).Returns(new DateTime(2015, 06, 01));
               yield return new TestCaseData(new DateTime(2015, 07, 01)).Returns(new DateTime(2015, 07, 01));
               yield return new TestCaseData(new DateTime(2015, 08, 01)).Returns(new DateTime(2015, 08, 01));
               yield return new TestCaseData(new DateTime(2015, 09, 01)).Returns(new DateTime(2015, 09, 01));
               yield return new TestCaseData(new DateTime(2015, 10, 01)).Returns(new DateTime(2015, 10, 01));
               yield return new TestCaseData(new DateTime(2015, 11, 01)).Returns(new DateTime(2015, 11, 01));
               yield return new TestCaseData(new DateTime(2015, 12, 01)).Returns(new DateTime(2015, 12, 01));
               yield return new TestCaseData(new DateTime(2015, 12, 02)).Returns(new DateTime(2016, 1, 1));
           }
       }

       public IEnumerable FirstDayOfNextMonth
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 01, 01)).Returns(new DateTime(2015, 02, 01));
               yield return new TestCaseData(new DateTime(2015, 02, 01)).Returns(new DateTime(2015, 03, 01));
               yield return new TestCaseData(new DateTime(2015, 03, 01)).Returns(new DateTime(2015, 04, 01));
               yield return new TestCaseData(new DateTime(2015, 04, 01)).Returns(new DateTime(2015, 05, 01));
               yield return new TestCaseData(new DateTime(2015, 05, 01)).Returns(new DateTime(2015, 06, 01));
               yield return new TestCaseData(new DateTime(2015, 06, 01)).Returns(new DateTime(2015, 07, 01));
               yield return new TestCaseData(new DateTime(2015, 07, 01)).Returns(new DateTime(2015, 08, 01));
               yield return new TestCaseData(new DateTime(2015, 08, 01)).Returns(new DateTime(2015, 09, 01));
               yield return new TestCaseData(new DateTime(2015, 09, 01)).Returns(new DateTime(2015, 10, 01));
               yield return new TestCaseData(new DateTime(2015, 10, 01)).Returns(new DateTime(2015, 11, 01));
               yield return new TestCaseData(new DateTime(2015, 11, 01)).Returns(new DateTime(2015, 12, 01));
               yield return new TestCaseData(new DateTime(2015, 12, 01)).Returns(new DateTime(2016, 1, 1));
               yield return new TestCaseData(new DateTime(2015, 12, 02)).Returns(new DateTime(2016, 1, 1));
               yield return new TestCaseData(new DateTime(2015, 02, 28)).Returns(new DateTime(2015, 03, 01));
               yield return new TestCaseData(new DateTime(2015, 01, 5)).Returns(new DateTime(2015, 02, 01));
               yield return new TestCaseData(new DateTime(2015, 02, 12)).Returns(new DateTime(2015, 03, 01));
               yield return new TestCaseData(new DateTime(2015, 03, 20)).Returns(new DateTime(2015, 04, 01));
               yield return new TestCaseData(new DateTime(2015, 04, 21)).Returns(new DateTime(2015, 05, 01));
               yield return new TestCaseData(new DateTime(2015, 05, 30)).Returns(new DateTime(2015, 06, 01));
               yield return new TestCaseData(new DateTime(2015, 06, 4)).Returns(new DateTime(2015, 07, 01));
           }
       }

       public IEnumerable DaysBetween
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 01, 01), new DateTime(2015, 01, 03)).Returns(2);
               yield return new TestCaseData(new DateTime(2015, 01, 01), new DateTime(2014, 12, 31)).Returns(-1);
               yield return new TestCaseData(new DateTime(2015, 01, 01), new DateTime(2015, 01, 06)).Returns(5);
           }
       }

       public IEnumerable MonthBetween
       {
           get
           {
               yield return new TestCaseData(new DateTime(2015, 01, 01), new DateTime(2015, 03, 03)).Returns(2);
               yield return new TestCaseData(new DateTime(2015, 01, 21), new DateTime(2015, 03, 03)).Returns(2);
               yield return new TestCaseData(new DateTime(2015, 01, 21), new DateTime(2015, 01, 03)).Returns(0);
               yield return new TestCaseData(new DateTime(2015, 03, 21), new DateTime(2015, 01, 03)).Returns(2);
           }
       }
    }
}
