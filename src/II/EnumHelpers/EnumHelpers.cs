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
using System.Linq;

using PPWCode.Util.OddsAndEnds.II.Extensions;

namespace PPWCode.Util.OddsAndEnds.II.EnumHelpers
{
    /// <summary>
    /// Helper class for Enumeration.
    /// </summary>
    public static class EnumHelpers
    {
        /// <summary>
        /// Returns enumeration as IEnumerable of T.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <returns>IEnumerable of T.</returns>
        [Obsolete(@"Use EnumHelpers.AsEnumerable")]
        public static IEnumerable<T> EnumEnumerable<T>()
        {
            return AsEnumerable<T>();
        }

        /// <summary>
        /// Returns enumeration as IEnumerable of T.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <returns>IEnumerable of T.</returns>
        public static IEnumerable<T> AsEnumerable<T>()
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidCastException("Enum derived type expected.");
            }

            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// <summary>
        /// Gets localized description of value of enumeration.
        /// </summary>
        /// <param name="enumValue">The type of the enumeration.</param>
        /// <returns>The localized description.</returns>
        [Obsolete(@"Use EnumExtension.GetLocalizedDescription()")]
        public static string GetLocalizedDescription(this Enum enumValue)
        {
            return EnumExtension.GetLocalizedDescription(enumValue);
        }
    }
}