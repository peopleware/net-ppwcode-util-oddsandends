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
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;

namespace PPWCode.Util.OddsAndEnds.II.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Gets the fully qualified name for a given type.
        /// </summary>
        /// <param name="value">The given type.</param>
        /// <returns>The fully qualified name for the given type <paramref name="value"/>.</returns>
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
                                    && (t.GetCustomAttributes(false).OfType<DataContractAttribute>().Any()
                                       || t.GetCustomAttributes(false).OfType<CollectionDataContractAttribute>().Any()));

                IEnumerable<Type> genericBoundedTypes =
                    foundTypes
                        .Select(t => genericUnboundedType.MakeGenericType(t));

                knownTypes.AddRange(foundTypes);
                knownTypes.AddRange(genericBoundedTypes);
            }

            return knownTypes;
        }

        /// <summary>
        ///     Check whether a type is a nullable type.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <returns>A boolean indicating whether the given <paramref name="type"/> is nullable.</returns>
        public static bool IsNullableType(Type type)
        {
            Contract.Requires(type != null);

            // ReSharper disable PossibleNullReferenceException
            return type.IsGenericType
                   && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        ///     Get a sequence of types where all the items are a sub-class of <paramref name="type" />.
        ///     The parameter <paramref name="excludeSystemTypes" /> determines if system types should be excluded.
        /// </summary>
        /// <param name="type">The given type.</param>
        /// <param name="excludeSystemTypes">Parameter indicating whether system types should be excluded from the result.</param>
        /// <returns>An enumeration of the sub classes of the given <paramref name="type"/>, including or
        /// excluding the system types, depending on the given parameter <paramref name="excludeSystemTypes"/>.</returns>
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