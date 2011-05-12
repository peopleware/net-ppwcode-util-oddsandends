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
    public static class CollectionExtensions
    {
        [Pure]
        public static List<T> AsList<T>(this IEnumerable<T> items)
        {
            return items == null ? new List<T>() : items.ToList();
        }

        [Pure]
        public static IList<T> AsIList<T>(this IEnumerable<T> items)
        {
            return items == null ? new List<T>() : items.ToList();
        }

        [Pure]
        public static decimal? NullableSum(this IEnumerable<decimal?> items)
        {
            return items.Aggregate((decimal?)0, (s, x) => s + x);
        }

        [Pure]
        public static int? NullableSum(this IEnumerable<int?> items)
        {
            return items.Aggregate((int?)0, (s, x) => s + x);
        }

        [Pure]
        public static bool SetEqual<T>(this IEnumerable<T> outerSequence, IEnumerable<T> innerSequence)
        {
            return (outerSequence.Count() == innerSequence.Count())
                   && (outerSequence.Except(innerSequence).Count() == 0)
                   && (innerSequence.Except(outerSequence).Count() == 0);
        }

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

        [Pure]
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }
            return IsEmptyOperator(items);
        }

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