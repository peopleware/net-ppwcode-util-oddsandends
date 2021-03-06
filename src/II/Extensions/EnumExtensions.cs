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
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace PPWCode.Util.OddsAndEnds.II.Extensions
{
    /// <summary>
    ///     Class that provides extensions for Enumeration.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        ///     Gets the localized description for given value.
        /// </summary>
        /// <param name="enumValue">The given value.</param>
        /// <returns>The localized description.</returns>
        public static string GetLocalizedDescription(this Enum enumValue)
        {
            if (enumValue != null)
            {
                System.ComponentModel.TypeConverter tc = TypeDescriptor.GetConverter(enumValue.GetType());
                return tc != null
                           ? tc.ConvertToString(enumValue)
                           : enumValue.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Gets the localized description for given value and specified CultureInfo.
        /// </summary>
        /// <param name="enumValue">The given value.</param>
        /// <param name="ci">The CultureInfo.</param>
        /// <returns>The localized value.</returns>
        public static string GetLocalizedDescription(this Enum enumValue, CultureInfo ci)
        {
            if (enumValue != null)
            {
                System.ComponentModel.TypeConverter tc = TypeDescriptor.GetConverter(enumValue.GetType());
                return tc != null
                           ? tc.ConvertToString(null, ci, enumValue)
                           : enumValue.ToString();
            }

            return string.Empty;
        }

        public static IEnumerable<Enum> GetFlags(this Enum value)
        {
            return GetFlags(value, Enum.GetValues(value.GetType()).Cast<Enum>().ToArray());
        }

        public static IEnumerable<Enum> GetIndividualFlags(this Enum value)
        {
            return GetFlags(value, GetFlagValues(value.GetType()).ToArray());
        }

        private static IEnumerable<Enum> GetFlags(Enum value, Enum[] values)
        {
            ulong bits = Convert.ToUInt64(value);
            List<Enum> results = new List<Enum>();
            for (int i = values.Length - 1; i >= 0; i--)
            {
                ulong mask = Convert.ToUInt64(values[i]);
                if (i == 0 && mask == 0L)
                {
                    break;
                }

                if ((bits & mask) == mask)
                {
                    results.Add(values[i]);
                    bits -= mask;
                }
            }

            if (bits != 0L)
            {
                return Enumerable.Empty<Enum>();
            }

            if (Convert.ToUInt64(value) != 0L)
            {
                return results.Reverse<Enum>();
            }

            if (bits == Convert.ToUInt64(value) && values.Length > 0 && Convert.ToUInt64(values[0]) == 0L)
            {
                return values.Take(1);
            }

            return Enumerable.Empty<Enum>();
        }

        private static IEnumerable<Enum> GetFlagValues(Type enumType)
        {
            ulong flag = 0x1;
            foreach (Enum value in Enum.GetValues(enumType).Cast<Enum>())
            {
                ulong bits = Convert.ToUInt64(value);
                if (bits == 0L)
                {
                    // skip the zero value
                    continue;
                }

                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits)
                {
                    yield return value;
                }
            }
        }
    }
}