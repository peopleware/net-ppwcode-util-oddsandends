// Copyright 2010-2015 by PeopleWare n.v..
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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace PPWCode.Util.OddsAndEnds.I.TypeConverter
{
    /// <summary>
    ///     Translates a <see cref="Enum"/> to a description using the current language.
    /// </summary>
    public class LocalizedEnumConverter :
        EnumConverter
    {
        /// <summary>
        ///     Indicates whether the values of the <see cref="Enum"/> are flags.
        /// </summary>
        private readonly bool m_IsFlagEnum;

        /// <summary>
        ///     Contains the values of the <see cref="Enum"/> in case that it is a flag <see cref="Enum"/>.
        /// </summary>
        private readonly Array m_FlagValues;

        /// <summary>
        ///     Lookup table which allows converting the localized text back to the <see cref="Enum"/> values.
        /// </summary>
        private readonly Dictionary<string, object> m_LookupTable;

        /// <summary>
        ///     Resource manager depending on the converted <see cref="Enum"/> type.
        /// </summary>
        private readonly ResourceManager m_ResourceManager;

        /// <summary>
        ///     Create a new instance of the converter using translations from the given resource manager.
        /// </summary>
        /// <param name="enumType">The given type.</param>
        public LocalizedEnumConverter(Type enumType)
            : base(enumType)
        {
            Assembly assembly = enumType.Assembly;
            string resourceBaseName = string.Format(CultureInfo.InvariantCulture, "{0}.Properties.Resources", assembly.GetName().Name);
            m_ResourceManager = assembly.GetManifestResourceNames().Any(o => o.StartsWith(resourceBaseName))
                                    ? new ResourceManager(resourceBaseName, enumType.Assembly)
                                    : null;

            m_LookupTable = new Dictionary<string, object>();
            ICollection standardValues = GetStandardValues();
            if (standardValues != null)
            {
                foreach (object value in standardValues)
                {
                    string text = GetLocalizedValueText(value, CultureInfo.CurrentCulture);
                    if (text != null)
                    {
                        m_LookupTable.Add(text, value);
                    }
                }
            }

            if (enumType.GetCustomAttributes(typeof(FlagsAttribute), true).Length <= 0)
            {
                return;
            }

            m_IsFlagEnum = true;
            m_FlagValues = Enum.GetValues(enumType);
        }

        /// <summary>
        ///     Return true if the given value is can be represented using a single bit.
        /// </summary>
        /// <param name="value">The given value.</param>
        /// <returns>A <see cref="bool"/> indicating whether the value can be represented as a single bit.</returns>
        private static bool IsSingleBitValue(ulong value)
        {
            switch (value)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    return (value & (value - 1)) == 0;
            }
        }

        /// <summary>
        ///     Return the <see cref="Enum"/> value for a flagged <see cref="Enum"/>.
        /// </summary>
        /// <param name="text">The text to convert (can also be a comma separated list of multiple flags).</param>
        /// <returns>The <see cref="Enum"/> value.</returns>
        private object GetFlagValueFromLocalizedString(string text)
        {
            ulong result = 0;
            foreach (string textValue in text.Split(','))
            {
                object value;
                if (!m_LookupTable.TryGetValue(textValue.Trim(), out value))
                {
                    return null;
                }

                result |= Convert.ToUInt32(value, CultureInfo.InvariantCulture);
            }

            return Enum.ToObject(EnumType, result);
        }

        /// <summary>
        ///     Return the text to display for a flag value in the given culture.
        /// </summary>
        /// <param name="value">The flag <see cref="Enum"/> value to get the text for.</param>
        /// <param name="ci">The given culture.</param>
        /// <returns>The localized text.</returns>
        private string GetFlagValueText(object value, CultureInfo ci)
        {
            if (value != null)
            {
                // if there is a standard value then use it
                if (Enum.IsDefined(value.GetType(), value))
                {
                    return GetLocalizedValueText(value, ci);
                }

                // otherwise find the combination of flag bit values
                // that makes up the value
                ulong numericValue = Convert.ToUInt32(value, CultureInfo.InvariantCulture);
                string result = null;
                foreach (object flagValue in m_FlagValues)
                {
                    ulong numericFlagValue = Convert.ToUInt32(flagValue, CultureInfo.InvariantCulture);

                    if (IsSingleBitValue(numericFlagValue) && ((numericFlagValue & numericValue) == numericFlagValue))
                    {
                        string valueText = GetLocalizedValueText(flagValue, ci);
                        result = result == null
                                     ? valueText
                                     : string.Format(CultureInfo.InvariantCulture, "{0}, {1}", result, valueText);
                    }
                }

                return result;
            }

            return string.Empty;
        }

        /// <summary>
        ///     Return the <see cref="Enum"/> value for a simple (non-flagged <see cref="Enum"/>).
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>The <see cref="Enum"/> value.</returns>
        private object GetValueFromLocalizedString(string text)
        {
            object result;

            if (!m_LookupTable.TryGetValue(text, out result))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        ///     Return the text to display for a simple value in the given culture.
        /// </summary>
        /// <param name="value">The <see cref="Enum"/> value to get the text for.</param>
        /// <param name="ci">The given culture.</param>
        /// <returns>The localized text.</returns>
        private string GetLocalizedValueText(object value, CultureInfo ci)
        {
            if (value != null)
            {
                if (m_ResourceManager != null)
                {
                    Type type = value.GetType();
                    string resourceName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", type.Name, value);
                    return m_ResourceManager.GetString(resourceName, ci) ?? value.ToString();
                }

                return value.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        ///     Convert string values to <see cref="Enum"/> values.
        /// </summary>
        /// <param name="context">The given context.</param>
        /// <param name="culture">The given culture.</param>
        /// <param name="value">The given value.</param>
        /// <returns>A object that could be constructed from the string.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string valueAsString = value as string;
            object result = null;
            if (valueAsString != null)
            {
                result = m_IsFlagEnum
                             ? GetFlagValueFromLocalizedString(valueAsString)
                             : GetValueFromLocalizedString(valueAsString);
            }

            return result ?? base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        ///     Convert the given value to another type.
        /// </summary>
        /// <param name="context">The given context.</param>
        /// <param name="ci">The given culture.</param>
        /// <param name="value">The given value.</param>
        /// <param name="destinationType">The given destination type.</param>
        /// <returns>An object of the given type that could be constructed from the given value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo ci, object value, Type destinationType)
        {
            if (destinationType.Equals(typeof(string)))
            {
                return m_IsFlagEnum
                           ? GetFlagValueText(value, ci)
                           : GetLocalizedValueText(value, ci);
            }

            return base.ConvertTo(context, ci, value, destinationType);
        }
    }
}