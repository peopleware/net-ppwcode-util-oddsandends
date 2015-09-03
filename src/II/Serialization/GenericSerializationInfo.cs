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

using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.II.Serialization
{
    /// <summary>
    ///     Class for generic serialization info.
    /// </summary>
    public class GenericSerializationInfo
    {
        private readonly SerializationInfo m_SerializationInfo;

        /// <summary>
        ///     The constructor.
        /// </summary>
        /// <param name="info">The SerializationInfo.</param>
        public GenericSerializationInfo(SerializationInfo info)
        {
            m_SerializationInfo = info;
        }

        /// <summary>
        ///     Adds a value into the SerializationInfo store.
        /// </summary>
        /// <typeparam name="T">The type declaration.</typeparam>
        /// <param name="name">The name to associate with the value.</param>
        /// <param name="value">The value to be serialized.</param>
        public void AddValue<T>(string name, T value)
        {
            m_SerializationInfo.AddValue(name, value, typeof(T));
        }

        /// <summary>
        ///     Retrieves a value from the SerializationInfo store.
        /// </summary>
        /// <typeparam name="T">The type declaration.</typeparam>
        /// <param name="name">The name associated with the value to retrieve.</param>
        /// <returns>The object of the specified Type associated with the name.</returns>
        public T GetValue<T>(string name)
        {
            object obj = m_SerializationInfo.GetValue(name, typeof(T));
            return (T)obj;
        }
    }
}