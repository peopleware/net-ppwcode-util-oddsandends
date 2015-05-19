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
    /// Class that provides extensions for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Converts IEnumerable of T to list of T.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="items">IEnumerable of T.</param>
        /// <returns>List of T.</returns>
        [Pure]
        public static List<T> AsList<T>(this IEnumerable<T> items)
        {
            return items == null ? new List<T>() : items.ToList();
        }

        /// <summary>
        /// Converts IEnumerable of T to IList of T.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="items">IEnumerable of T.</param>
        /// <returns>IList of T.</returns>
        [Pure]
        public static IList<T> AsIList<T>(this IEnumerable<T> items)
        {
            return items == null ? new List<T>() : items.ToList();
        }

        /// <summary>
        /// Calculates the sum of nullable decimals.
        /// </summary>
        /// <param name="items">Nullable decimals.</param>
        /// <returns>Sum as nullable decimal.</returns>
        [Pure]
        public static decimal? NullableSum(this IEnumerable<decimal?> items)
        {
            return items.Aggregate((decimal?)0, (s, x) => s + x);
        }

        /// <summary>
        /// Calculates the sum of nullable integers.
        /// </summary>
        /// <param name="items">Nullable integers.</param>
        /// <returns>Sum of as nullable integer.</returns>
        [Pure]
        public static int? NullableSum(this IEnumerable<int?> items)
        {
            return items.Aggregate((int?)0, (s, x) => s + x);
        }

        /// <summary>
        /// Checks whether 2 IEnumerable of T are equal.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="outerSequence">The first IEnumerable of T.</param>
        /// <param name="innerSequence">The second IEnumerable of T.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool SetEqual<T>(this IEnumerable<T> outerSequence, IEnumerable<T> innerSequence)
        {
            return (outerSequence.Count() == innerSequence.Count())
                   && (outerSequence.Except(innerSequence).Count() == 0)
                   && (innerSequence.Except(outerSequence).Count() == 0);
        }

        /// <summary>
        /// Checks whether 2 IEnumerable of T are equal given a comparer.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="outerSequence">The first IEnumerable of T.</param>
        /// <param name="innerSequence">The second IEnumerable of T.</param>
        /// <param name="comparer">The equality comparer.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool SetEqual<T>(this IEnumerable<T> outerSequence, IEnumerable<T> innerSequence, IEqualityComparer<T> comparer)
        {
            return (outerSequence.Count() == innerSequence.Count())
                   && (outerSequence.Except(innerSequence, comparer).Count() == 0)
                   && (innerSequence.Except(outerSequence, comparer).Count() == 0);
        }

        private static bool IsEmptyOperator<T>(this IEnumerable<T> items)
        {
            ICollection<T> collection = items as ICollection<T>;
            if (collection != null)
            {
                return collection.Count == 0;
            }

            using (IEnumerator<T> enumerator = items.GetEnumerator())
            {
                return !enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Checks whether all items are empty.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="items">IEnumerable of T.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }

            return IsEmptyOperator(items);
        }

        /// <summary>
        /// Checks whether all items if IEnumerable or null or empty.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="items">IEnumerable of T.</param>
        /// <returns>True or false.</returns>
        [Pure]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || IsEmptyOperator(items);
        }

        private static IEnumerable<IGrouping<int, T>> SegmentIterator<T>(IEnumerable<T> source, int segments)
        {
            // calculate the number of elements per segment
            int count = source.Count();
            int perSegment = (int)Math.Ceiling((decimal)count / segments);

            // build the empty groups
            Grouping<int, T>[] groups = new Grouping<int, T>[segments];
            for (int i = 0; i < segments; i++)
            {
                Grouping<int, T> g = new Grouping<int, T>(perSegment)
                                     {
                                         Key = i + 1
                                     };

                groups[i] = g;
            }

            // fill the groups and yield results
            // when each group is full.
            int index = 0;
            int segment = 1;
            Grouping<int, T> group = groups[0];
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    group.Add(e.Current);
                    index++;

                    // yield return when we have filled each group
                    if ((segment < segments) &&
                        (index == perSegment))
                    {
                        yield return group;
                        index = 0;
                        segment++;
                        group = groups[segment - 1];
                    }
                }
            }

            // return the last and any remaining groups
            // (these will be empty or partially populated)
            while (segment <= segments)
            {
                yield return groups[segment - 1];
                segment++;
            }
        }

        [Pure]
        public static IEnumerable<IGrouping<int, T>> Segment<T>(this IEnumerable<T> source, int segments)
        {
            if (source == null)
            {
                throw new ArgumentNullException(@"source");
            }

            if (segments <= 0)
            {
                throw new ArgumentOutOfRangeException(@"segments");
            }

            return SegmentIterator(source, segments);
        }
    }
}