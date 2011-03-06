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
        public static List<T> AsList<T>(this IEnumerable<T> collection)
        {
            return collection == null ? new List<T>() : collection.ToList();
        }

        [Pure]
        public static IList<T> AsIList<T>(this IEnumerable<T> collection)
        {
            return collection == null ? new List<T>() : collection.ToList();
        }

        [Pure]
        public static decimal? NullableSum(this IEnumerable<decimal?> ienumerable)
        {
            return ienumerable.Aggregate((decimal?)0, (s, x) => s + x);
        }

        [Pure]
        public static int? NullableSum(this IEnumerable<int?> ienumerable)
        {
            return ienumerable.Aggregate((int?)0, (s, x) => s + x);
        }

        [Pure]
        public static bool SetEqual<T>(this IEnumerable<T> ienumerable, IEnumerable<T> other)
        {
            return (ienumerable.Count() == other.Count())
                   && (ienumerable.Except(other).Count() == 0)
                   && (other.Except(ienumerable).Count() == 0);
        }

        [Pure]
        public static bool SetEqual<T>(this IEnumerable<T> ienumerable, IEnumerable<T> other, IEqualityComparer<T> comparer)
        {
            return (ienumerable.Count() == other.Count())
                   && (ienumerable.Except(other, comparer).Count() == 0)
                   && (other.Except(ienumerable, comparer).Count() == 0);
        }

        [Pure]
        public static bool IsEmpty<T>(this IEnumerable<T> ienumerable)
        {
            if (ienumerable == null)
            {
                throw new ArgumentNullException();
            }
            ICollection<T> collection = ienumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count == 0;
            }
            using (IEnumerator<T> enumerator = ienumerable.GetEnumerator())
            {
                return !enumerator.MoveNext();
            }
        }

        [Pure]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> ienumerable)
        {
            if (ienumerable == null)
            {
                return true;
            }
            ICollection<T> collection = ienumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count == 0;
            }
            using (IEnumerator<T> enumerator = ienumerable.GetEnumerator())
            {
                return !enumerator.MoveNext();
            }
        }
    }
}
