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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PPWCode.Util.OddsAndEnds.II.Extensions
{
    /// <summary>
    ///     Helper class for DateTime.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        ///     The earliest of 2 date-times.
        /// </summary>
        /// <returns>
        ///     The earliest of <paramref name="dt1" /> and
        ///     <paramref name="dt2" />.
        /// </returns>
        /// <param name="dt1">The first given date.</param>
        /// <param name="dt2">The second given date.</param>
        /// <returns>The minimum of the given dates.</returns>
        [Pure]
        public static DateTime Min(DateTime dt1, DateTime dt2)
        {
            Contract.Ensures(Contract.Result<DateTime>() == (dt1 < dt2 ? dt1 : dt2));

            return dt1 < dt2 ? dt1 : dt2;
        }

        /// <summary>
        ///     The earliest of 2 date-times. When <paramref name="dt2" />
        ///     is <c>null</c>, we return <paramref name="dt2" />.
        /// </summary>
        /// <param name="dt1">The first given date.</param>
        /// <param name="dt2">The second given date.</param>
        /// <returns>
        ///     The earliest of <paramref name="dt1" /> and
        ///     <paramref name="dt2" />. <paramref name="dt2" /> when
        ///     <paramref name="dt1" /> is <c>null</c>.
        /// </returns>
        [Pure]
        public static DateTime Min(DateTime? dt1, DateTime dt2)
        {
            Contract.Ensures(Contract.Result<DateTime>() ==
                             (dt1.HasValue ? (dt1.Value < dt2 ? dt1.Value : dt2) : dt2));

            return dt1.HasValue
                       ? dt1.Value < dt2 ? dt1.Value : dt2
                       : dt2;
        }

        /// <summary>
        ///     The earliest of 2 date-times. When <paramref name="dt2" />
        ///     is <c>null</c>, we return <paramref name="dt1" />.
        /// </summary>
        /// <param name="dt1">The first given date.</param>
        /// <param name="dt2">The second given date.</param>
        /// <returns>
        ///     The earliest of <paramref name="dt1" /> and
        ///     <paramref name="dt2" />. <paramref name="dt1" /> when
        ///     <paramref name="dt2" /> is <c>null</c>.
        /// </returns>
        [Pure]
        public static DateTime Min(DateTime dt1, DateTime? dt2)
        {
            Contract.Ensures(Contract.Result<DateTime>() ==
                             (dt2.HasValue ? (dt1 < dt2.Value ? dt1 : dt2.Value) : dt1));

            return Min(dt2, dt1);
        }

        /// <summary>
        ///     The earliest of a number of date-times.
        /// </summary>
        /// <param name="dt">An array of dates.</param>
        /// <returns>
        ///     With zero inputs, <see cref="DateTime.MinValue" />
        ///     is returned.
        /// </returns>
        [Pure]
        public static DateTime? Min(params DateTime[] dt)
        {
            Contract.Ensures(Contract.Result<DateTime?>() == (dt.Length == 0 ? null : (DateTime?)dt.Min()));

            return dt.Length == 0
                       ? null
                       : (DateTime?)dt.Min();
        }

        /// <summary>
        ///     The earliest of a number of date-times.
        /// </summary>
        /// <param name="dt">An array of dates.</param>
        /// <returns>
        ///     <c>null</c> values are skipped.
        ///     With zero inputs, or all <c>null</c> inputs,<c>null</c>
        ///     is returned.
        /// </returns>
        [Pure]
        public static DateTime? Min(params DateTime?[] dt)
        {
            // ReSharper disable PossibleInvalidOperationException
            IEnumerable<DateTime> dts = dt.Where(o => o.HasValue).Select(o => o.Value);
            // ReSharper restore PossibleInvalidOperationException
            return !dts.IsEmpty()
                       ? dts.Min()
                       : default(DateTime?);
        }

        /// <summary>
        ///     Gets the biggest DateTime out of 2 DateTimes.
        /// </summary>
        /// <param name="dt1">The first DateTime.</param>
        /// <param name="dt2">The second DateTime.</param>
        /// <returns>The biggest DateTime.</returns>
        [Pure]
        public static DateTime Max(DateTime dt1, DateTime dt2)
        {
            return dt1 > dt2 ? dt1 : dt2;
        }

        /// <summary>
        ///     Gets the biggest DateTime out of 2 DateTimes of which the first DateTime is nullable.
        /// </summary>
        /// <param name="dt1">The first nullable DateTime.</param>
        /// <param name="dt2">The second DateTime.</param>
        /// <returns>The biggest DateTime.</returns>
        [Pure]
        public static DateTime Max(DateTime? dt1, DateTime dt2)
        {
            return dt1.HasValue
                       ? dt1.Value > dt2 ? dt1.Value : dt2
                       : dt2;
        }

        /// <summary>
        ///     Gets the biggest DateTime out of 2 DateTimes of which the second DateTime is nullable.
        /// </summary>
        /// <param name="dt1">The first DateTime.</param>
        /// <param name="dt2">The second nullable DateTime.</param>
        /// <returns>The biggest DateTime.</returns>
        [Pure]
        public static DateTime Max(DateTime dt1, DateTime? dt2)
        {
            return Max(dt2, dt1);
        }

        /// <summary>
        ///     Gets the biggest DateTime out of array of DateTimes.
        /// </summary>
        /// <param name="dt">Array of DateTimes.</param>
        /// <returns>The biggest DateTime or null.</returns>
        [Pure]
        public static DateTime? Max(params DateTime[] dt)
        {
            return dt.Length == 0
                       ? null
                       : (DateTime?)dt.Max();
        }

        /// <summary>
        ///     The latest of a number of date-times.
        /// </summary>
        /// <param name="dt">An array of dates.</param>
        /// <returns>
        ///     <c>null</c> values are skipped.
        ///     With zero inputs, or all <c>null</c> inputs,<c>null</c>
        ///     is returned.
        /// </returns>
        [Pure]
        public static DateTime? Max(params DateTime?[] dt)
        {
            // ReSharper disable PossibleInvalidOperationException
            IEnumerable<DateTime> dts = dt.Where(o => o.HasValue).Select(o => o.Value);
            // ReSharper restore PossibleInvalidOperationException
            return !dts.IsEmpty()
                       ? dts.Max()
                       : default(DateTime?);
        }
    }
}