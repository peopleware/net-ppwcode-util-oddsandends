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
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if the first character of a string is a capital
        /// </summary>
        [Pure]
        public static string GetQualifiedName(this Type value)
        {
            Contract.Requires(value != null);

            string fullName = value.FullName;
            string assemblyName = value.Assembly.FullName.Split(',')[0];
            return string.Concat(fullName, ", ", assemblyName);
        }

        public static IEnumerable<Type> GetKnownTypes(this IEnumerable<Assembly> assemblies, Type genericUnboundedType)
        {
            List<Type> knownTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> foundTypes =
                    assembly
                        .GetTypes()
                        .Where(t => t.IsClass
                                    && t.IsPublic
                                    && t.GetCustomAttributes(false)
                                           .OfType<DataContractAttribute>()
                                           .Any());

                IEnumerable<Type> genericBoundedTypes =
                    foundTypes
                        .Select(t => genericUnboundedType.MakeGenericType(t));

                knownTypes.AddRange(foundTypes);
                knownTypes.AddRange(genericBoundedTypes);
            }
            return knownTypes;
        }

        /// <summary>
        /// Check if a type is of Nullable type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type type)
        {
            Contract.Requires(type != null);

            // ReSharper disable PossibleNullReferenceException
            return type.IsGenericType
                   && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        /// Get a sequence of types where all the items are a sub-class of <paramref name="type"/>.
        /// The parameter <paramref name="excludeSystemTypes"/> determines if system types should be excluded.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="excludeSystemTypes"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetSubclassesOf(this Type type, bool excludeSystemTypes)
        {
            if (type == null)
            {
                throw new ArgumentNullException();
            }
            return Thread
                .GetDomain()
                .GetAssemblies()
                .Where(a => !excludeSystemTypes || (excludeSystemTypes && !a.FullName.StartsWith("System.")))
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsSubclassOf(type));
        }

        [Pure]
        public static bool IsSuperTypeOf(this Type t1, Type t2)
        {
            if (t1 == null)
            {
                throw new ArgumentNullException();
            }
            return t1.IsAssignableFrom(t2);
        }
    }
}
