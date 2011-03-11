//Copyright 2004 - $Date: 2008-11-15 23:58:07 +0100 (za, 15 nov 2008) $ by PeopleWare n.v..

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.Extensions
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// The earliest of 2 date-times.
        /// </summary>
        /// <returns>The earliest of <paramref name="dt1"/> and
        /// <paramref name="dt2"/>.</returns>
        [Pure]
        public static DateTime Min(DateTime dt1, DateTime dt2)
        {
#if EXTRA_CONTRACTS
            Contract.Ensures(Contract.Result<DateTime>() == (dt1 < dt2 ? dt1 : dt2));
#endif

            return dt1 < dt2 ? dt1 : dt2;
        }

        /// <summary>
        /// The earliest of 2 date-times. When <paramref name="dt2"/>
        /// is <c>null</c>, we return <paramref name="dt2"/>.
        /// </summary>
        /// <returns>The earliest of <paramref name="dt1"/> and
        /// <paramref name="dt2"/>. <paramref name="dt2"/> when
        /// <paramref name="dt1"/> is <c>null</c>.</returns>
        [Pure]
        public static DateTime Min(DateTime? dt1, DateTime dt2)
        {
#if EXTRA_CONTRACTS
            Contract.Ensures(Contract.Result<DateTime>() ==
                (dt1.HasValue ? (dt1.Value < dt2 ? dt1.Value : dt2) : dt2));
#endif

            return dt1.HasValue
                       ? dt1.Value < dt2 ? dt1.Value : dt2
                       : dt2;
        }

        /// <summary>
        /// The earliest of 2 date-times. When <paramref name="dt2"/>
        /// is <c>null</c>, we return <paramref name="dt1"/>.
        /// </summary>
        /// <returns>The earliest of <paramref name="dt1"/> and
        /// <paramref name="dt2"/>. <paramref name="dt1"/> when
        /// <paramref name="dt2"/> is <c>null</c>.</returns>
        [Pure]
        public static DateTime Min(DateTime dt1, DateTime? dt2)
        {
#if EXTRA_CONTRACTS
            Contract.Ensures(Contract.Result<DateTime>() ==
                (dt2.HasValue ? (dt1 < dt2.Value ? dt1 : dt2.Value) : dt1));
#endif

            return Min(dt2, dt1);
        }

        /// <summary>
        /// The earliest of a number of date-times.
        /// </summary>
        /// <returns>With zero inputs, <see cref="DateTime.MinValue"/>
        /// is returned.</returns>
        [Pure]
        public static DateTime? Min(params DateTime[] dt)
        {
#if EXTRA_CONTRACTS
            Contract.Ensures(Contract.Result<DateTime>() ==
                dt.Length == 0 ? DateTime.MinValue : dt.Min());
#endif

            return dt.Length == 0
                       ? null
                       : (DateTime?)dt.Min();
        }

        /// <summary>
        /// The earliest of a number of date-times.
        /// </summary>
        /// <returns><c>null</c> values are skipped.
        /// With zero inputs, or all <c>null</c> inputs,<c>null</c>
        /// is returned.</returns>
        [Pure]
        public static DateTime? Min(params DateTime?[] dt)
        {
#if EXTRA_CONTRACTS
            Contract.Ensures(Contract.Result<DateTime>() ==
                Min(dt.Where(o => o.HasValue).Select(o => o.Value).ToArray()));
#endif

            // ReSharper disable PossibleInvalidOperationException
            IEnumerable<DateTime> dts = dt.Where(o => o.HasValue).Select(o => o.Value);
            // ReSharper restore PossibleInvalidOperationException
            return !dts.IsEmpty()
                       ? dts.Min()
                       : default(DateTime?);
        }

        [Pure]
        public static DateTime Max(DateTime dt1, DateTime dt2)
        {
            return dt1 > dt2 ? dt1 : dt2;
        }

        [Pure]
        public static DateTime Max(DateTime? dt1, DateTime dt2)
        {
            return dt1.HasValue
                       ? dt1.Value > dt2 ? dt1.Value : dt2
                       : dt2;
        }

        [Pure]
        public static DateTime Max(DateTime dt1, DateTime? dt2)
        {
            return Max(dt2, dt1);
        }

        [Pure]
        public static DateTime? Max(params DateTime[] dt)
        {
            return dt.Length == 0
                       ? null
                       : (DateTime?)dt.Max();
        }

        /// <summary>
        /// The latest of a number of date-times.
        /// </summary>
        /// <returns><c>null</c> values are skipped.
        /// With zero inputs, or all <c>null</c> inputs,<c>null</c>
        /// is returned.</returns>
        [Pure]
        public static DateTime? Max(params DateTime?[] dt)
        {
#if EXTRA_CONTRACTS
            Contract.Ensures(Contract.Result<DateTime>() ==
                Max(dt.Where(o => o.HasValue).Select(o => o.Value).ToArray()));
#endif
            // ReSharper disable PossibleInvalidOperationException
            IEnumerable<DateTime> dts = dt.Where(o => o.HasValue).Select(o => o.Value);
            // ReSharper restore PossibleInvalidOperationException
            return !dts.IsEmpty()
                       ? dts.Max()
                       : default(DateTime?);
        }
    }
}
