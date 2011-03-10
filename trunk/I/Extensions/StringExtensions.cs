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
using System.Diagnostics.Contracts;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.Extensions
{
    /// <summary>
    /// PPW helper string functions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks if the first character of a string is a capital
        /// </summary>
        [Pure]
        public static bool StartWithACapital(this string value)
        {
            Contract.Requires(!string.IsNullOrEmpty(value));
            Contract.Ensures(Contract.Result<bool>() == Char.IsUpper(value[0]));

            return Char.IsUpper(value[0]);
        }

        /// <summary>
        /// Convert the string to an Int64
        /// The function returns NULL if the conversion fails
        /// </summary>
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
        /// Convert the string to an Int32
        /// The function returns NULL if the conversion fails
        /// </summary>
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
        /// Convert the string to a Decimal
        /// The function returns NULL if the conversion fails
        /// </summary>
        [Pure]
        public static decimal? ToDecimal(this string s)
        {
            decimal val;

            if (decimal.TryParse(s, out val))
            {
                return val;
            }

            return (int?)null;
        }

        /// <summary>
        /// Convert the string to a DateTime
        /// The function returns NULL if the conversion fails
        /// </summary>
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
