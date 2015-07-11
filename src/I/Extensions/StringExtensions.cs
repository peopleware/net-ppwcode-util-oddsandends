// Copyright 2010-2015 by PeopleWare n.v..
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

namespace PPWCode.Util.OddsAndEnds.I.Extensions
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
        /// <returns>A <see cref="bool"/> indicating whether the given string starts with a capital.</returns>
        [Pure]
        public static bool StartWithACapital(this string value)
        {
            Contract.Requires(!string.IsNullOrEmpty(value));
            Contract.Ensures(Contract.Result<bool>() == char.IsUpper(value[0]));

            return char.IsUpper(value[0]);
        }

        /// <summary>
        ///     Convert the <see cref="string"/> to an <see cref="System.Int64"/>.
        /// </summary>
        /// <param name="s">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>The converted string.</returns>
        [Pure]
        public static long? ToLong(this string s)
        {
            long val;

            if (long.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }

        /// <summary>
        ///     Convert the <see cref="string"/> to an <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="s">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>The converted string.</returns>
        [Pure]
        public static int? ToInt(this string s)
        {
            int val;

            if (int.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }

        /// <summary>
        ///     Convert the <see cref="string"/> to a <see cref="decimal"/>.
        /// </summary>
        /// <param name="s">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>The converted string.</returns>
        [Pure]
        public static decimal? ToDecimal(this string s)
        {
            decimal val;

            if (decimal.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }

        /// <summary>
        ///     Convert the <see cref="string"/> to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="s">The given string.</param>
        /// <remarks>The function returns NULL if the conversion fails.</remarks>
        /// <returns>The converted string.</returns>
        [Pure]
        public static DateTime? ToDateTime(this string s)
        {
            DateTime val;

            if (DateTime.TryParse(s, out val))
            {
                return val;
            }

            return null;
        }
    }
}