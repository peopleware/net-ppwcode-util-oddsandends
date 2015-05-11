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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PPWCode.Util.OddsAndEnds.II.Extensions
{
    /// <summary>
    /// Helper class for grouping.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the values.</typeparam>
    public class Grouping<TKey, TElement> :
        IGrouping<TKey, TElement>,
        IList<TElement>
    {
        private int m_Count;
        private TElement[] m_Elements;
        private TKey m_Key;

        public Grouping(int count)
        {
            m_Elements = new TElement[count];
        }

        internal TKey Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            for (int i = 0; i < m_Count; i++)
            {
                yield return m_Elements[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        TKey IGrouping<TKey, TElement>.Key
        {
            get { return m_Key; }
        }

        int ICollection<TElement>.Count
        {
            get { return m_Count; }
        }

        bool ICollection<TElement>.IsReadOnly
        {
            get { return true; }
        }

        void ICollection<TElement>.Add(TElement item)
        {
            throw new ReadOnlyException();
        }

        void ICollection<TElement>.Clear()
        {
            throw new ReadOnlyException();
        }

        bool ICollection<TElement>.Contains(TElement item)
        {
            return Array.IndexOf(m_Elements, item, 0, m_Count) >= 0;
        }

        void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex)
        {
            Array.Copy(m_Elements, 0, array, arrayIndex, m_Count);
        }

        bool ICollection<TElement>.Remove(TElement item)
        {
            throw new ReadOnlyException();
        }

        int IList<TElement>.IndexOf(TElement item)
        {
            return Array.IndexOf(m_Elements, item, 0, m_Count);
        }

        void IList<TElement>.Insert(int index, TElement item)
        {
            throw new ReadOnlyException();
        }

        void IList<TElement>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        TElement IList<TElement>.this[int index]
        {
            get
            {
                if (index < 0 || index >= m_Count)
                {
                    throw new ArgumentOutOfRangeException(@"index");
                }

                return m_Elements[index];
            }

            set { throw new ReadOnlyException(); }
        }

        internal void Add(TElement element)
        {
            if (m_Elements.Length == m_Count)
            {
                Array.Resize(ref m_Elements, m_Count * 2);
            }

            m_Elements[m_Count++] = element;
        }
    }
}