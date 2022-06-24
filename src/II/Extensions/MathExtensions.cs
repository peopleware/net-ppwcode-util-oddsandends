﻿// Copyright 2014 by PeopleWare n.v..
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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PPWCode.Util.OddsAndEnds.II.Extensions
{
    /// <summary>
    ///     Class that provides extensions for Math.
    /// </summary>
    public static class MathExtensions
    {
        [Pure]
        public static bool IsConsecutiveSequence<T>(
            this IEnumerable<T> lst,
            Func<T, decimal?> extractDateBegin,
            Func<T, decimal?> extractDateEnd) where T : class
        {
            Contract.Requires(extractDateBegin != null);
            Contract.Requires(extractDateEnd != null);

            if (lst.IsNullOrEmpty())
            {
                return true;
            }

            var sortedList = lst
                .Select(o => new
                             {
                                 StartRange = extractDateBegin(o) ?? decimal.MinValue,
                                 EndRange = extractDateEnd(o) ?? decimal.MaxValue
                             })
                .OrderBy(o => o.StartRange)
                .ToList();

            var previousItem = sortedList[0];
            if (previousItem.StartRange > previousItem.EndRange)
            {
                return false;
            }

            for (int i = 1; i < sortedList.Count; i++)
            {
                var item = sortedList[i];
                if (item.StartRange > item.EndRange
                    || item.StartRange != previousItem.EndRange)
                {
                    return false;
                }

                previousItem = item;
            }

            return true;
        }

        [Pure]
        public static double Root(this decimal d, int root)
        {
            return Math.Exp(Math.Log(Convert.ToDouble(d)) / root);
        }

        [Pure]
        public static double YearInterestFraction(this double yearlyInterestRateAsPercentage)
        {
            double result = yearlyInterestRateAsPercentage / 100d;
            return result;
        }

        [Pure]
        public static double YearInterestFraction(this double yearlyInterestRateAsPercentage, int nrYears)
        {
            return InterestRate(yearlyInterestRateAsPercentage, nrYears, 1d);
        }

        [Pure]
        public static decimal YearInterestFraction(this decimal yearlyInterestRateAsPercentage)
        {
            decimal result = yearlyInterestRateAsPercentage / 100m;
            return result;
        }

        [Pure]
        public static decimal YearInterestFraction(this decimal yearlyInterestRateAsPercentage, int nrYears)
        {
            return Convert.ToDecimal(Convert.ToDouble(yearlyInterestRateAsPercentage).YearInterestFraction(nrYears));
        }

        private static double InterestRate(double yearlyInterestRateAsPercentage, double x, double y)
        {
            double exponent = x / y;
            double b = 1d + yearlyInterestRateAsPercentage.YearInterestFraction();
            double result = Math.Pow(b, exponent) - 1d;
            return result;
        }

        [Pure]
        public static double DayInterestFraction(this double yearlyInterestRateAsPercentage, DateTime when, int nrDays)
        {
            Contract.Requires(nrDays >= 1 && nrDays <= when.NumberOfDaysInYear());

            int daysInYear = when.NumberOfDaysInYear();
            return InterestRate(yearlyInterestRateAsPercentage, nrDays, daysInYear);
        }

        [Pure]
        public static decimal DayInterestFraction(this decimal yearlyInterestRateAsPercentage, DateTime when, int nrDays)
        {
            Contract.Requires(nrDays >= 1 && nrDays <= when.NumberOfDaysInYear());

            return Convert.ToDecimal(Convert.ToDouble(yearlyInterestRateAsPercentage).DayInterestFraction(when, nrDays));
        }

        [Pure]
        public static double MonthInterestFraction(this double yearlyInterestRateAsPercentage, int nrMonths)
        {
            Contract.Requires(nrMonths >= 1 && nrMonths <= 12);

            return InterestRate(yearlyInterestRateAsPercentage, nrMonths, 12d);
        }

        [Pure]
        public static decimal MonthInterestFraction(this decimal yearlyInterestRateAsPercentage, int nrMonths)
        {
            Contract.Requires(nrMonths >= 1 && nrMonths <= 12);

            return Convert.ToDecimal(Convert.ToDouble(yearlyInterestRateAsPercentage).MonthInterestFraction(nrMonths));
        }

        [Pure]
        public static double QuarterInterestFraction(this double yearlyInterestRateAsPercentage, int nrQuarters)
        {
            Contract.Requires(nrQuarters >= 1 && nrQuarters <= 4);

            return InterestRate(yearlyInterestRateAsPercentage, nrQuarters, 4d);
        }

        [Pure]
        public static decimal QuarterInterestFraction(this decimal yearlyInterestRateAsPercentage, int nrQuarters)
        {
            Contract.Requires(nrQuarters >= 1 && nrQuarters <= 4);

            return Convert.ToDecimal(Convert.ToDouble(yearlyInterestRateAsPercentage).QuarterInterestFraction(nrQuarters));
        }
    }
}