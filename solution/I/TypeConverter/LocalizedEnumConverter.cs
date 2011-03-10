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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.TypeConverter
{
    /// <summary>
    /// Converts enums depending on the current language
    /// </summary>
    public class LocalizedEnumConverter :
        EnumConverter
    {
        /// <summary>
        /// Indicates wether the values of the enum are flags
        /// </summary>
        private readonly bool m_IsFlagEnum;

        /// <summary>
        /// Contains the values of the enum in case that it is a flag enum
        /// </summary>
        private readonly Array m_FlagValues;

        /// <summary>
        /// Lookup table which allows converting the localized text back to the enum values
        /// </summary>
        private readonly Dictionary<string, object> m_LookupTable;

        /// <summary>
        /// Resource manager depending on the converted enum type
        /// </summary>
        private readonly ResourceManager m_ResourceManager;

        /// <summary>
        /// Create a new instance of the converter using translations from the given resource manager
        /// </summary>
        /// <param name="enumType"></param>
        public LocalizedEnumConverter(Type enumType)
            : base(enumType)
        {
            string resourceBaseName = string.Format(CultureInfo.InvariantCulture, "{0}.Properties.Resources", enumType.Assembly.GetName().Name);
            m_ResourceManager = new ResourceManager(resourceBaseName, enumType.Assembly);

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
        /// Return the Enum value for a flagged enum
        /// </summary>
        /// <param name="text">The text to convert (can also be a comma separated list of multiple flags)</param>
        /// <returns>The enum value</returns>
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
        /// Return true if the given value is can be represented using a single bit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// Return the text to display for a flag value in the given culture
        /// </summary>
        /// <param name="value">The flag enum value to get the text for</param>
        /// <returns>The localized text</returns>
        private string GetFlagValueText(object value, CultureInfo ci)
        {
            // if there is a standard value then use it
            //
            if (Enum.IsDefined(value.GetType(), value))
            {
                return GetLocalizedValueText(value, ci);
            }

            // otherwise find the combination of flag bit values
            // that makes up the value
            //
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

        /// <summary>
        /// Return the Enum value for a simple (non-flagged enum)
        /// </summary>
        /// <param name="text">The text to convert</param>
        /// <returns>The enum value</returns>
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
        /// Return the text to display for a simple value in the given culture
        /// </summary>
        /// <param name="value">The enum value to get the text for</param>
        /// <returns>The localized text</returns>
        private string GetLocalizedValueText(object value, CultureInfo ci)
        {
            Type type = value.GetType();
            string resourceName = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", type.Name, value);
            return m_ResourceManager.GetString(resourceName, ci) ?? value.ToString();
        }

        /// <summary>
        /// Convert string values to enum values
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// Convert the enum value to a string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
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