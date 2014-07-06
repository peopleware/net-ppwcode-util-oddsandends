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
using System.Diagnostics.Contracts;

namespace PPWCode.Util.OddsAndEnds.II.Extensions
{
    /// <summary>
    ///     PPW helper string functions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Checks if the first character of a string is a capital.
        /// </summary>
        /// <param name="value">The given string.</param>
        /// <returns>A boolean indicating whether the first character of <paramref name="value"/> is a capital.</returns>
        [Pure]
        public static bool StartWithACapital(this string value)
        {
            Contract.Requires(!string.IsNullOrEmpty(value));
            Contract.Ensures(Contract.Result<bool>() == char.IsUpper(value[0]));

            return char.IsUpper(value[0]);
        }

        /// <summary>
        ///     Convert the string to an <c>Int64</c>.
        /// </summary>
        /// <param name="value">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>A number for which the given <paramref name="value"/> is the string representation.</returns>
        [Pure]
        public static long? ToLong(this string value)
        {
            long val;

            if (long.TryParse(value, out val))
            {
                return val;
            }

            return null;
        }

        /// <summary>
        ///     Convert the string to an <c>Int32</c>.
        /// </summary>
        /// <param name="value">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>A number for which the given <paramref name="value"/> is the string representation.</returns>
        [Pure]
        public static int? ToInt(this string value)
        {
            int val;

            if (int.TryParse(value, out val))
            {
                return val;
            }

            return null;
        }

        /// <summary>
        ///     Convert the string to an <c>Decimal</c>.
        /// </summary>
        /// <param name="value">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>A number for which the given <paramref name="value"/> is the string representation.</returns>
        [Pure]
        public static decimal? ToDecimal(this string value)
        {
            decimal val;

            if (decimal.TryParse(value, out val))
            {
                return val;
            }

            return null;
        }

        /// <summary>
        ///     Convert the string to an <c>DateTime</c>.
        /// </summary>
        /// <param name="value">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>A <c>DateTime</c> for which the given <paramref name="value"/> is the string representation.</returns>
        [Pure]
        public static DateTime? ToDateTime(this string value)
        {
            DateTime val;

            if (DateTime.TryParse(value, out val))
            {
                return val;
            }

            return null;
        }
    }
}