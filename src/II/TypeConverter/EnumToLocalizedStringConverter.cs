// Copyright 2015 by PeopleWare n.v..
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
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace PPWCode.Util.OddsAndEnds.II.TypeConverter
{
    /// <summary>
    ///     Converts <c>enum</c>s depending on the current language.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EnumToLocalizedStringConverter : EnumConverter
    {
        /// <summary>
        ///     Resource manager depending on the converted <c>enum</c> type.
        /// </summary>
        private readonly ResourceManager m_ResourceManager;

        /// <summary>
        ///     Create a new instance of the converter using translations from the given resource manager.
        /// </summary>
        /// <param name="enumType">The given <c>enum</c> type.</param>
        public EnumToLocalizedStringConverter(Type enumType)
            : base(enumType)
        {
            Assembly assembly = enumType.Assembly;
            string resourceBaseName = string.Format(CultureInfo.InvariantCulture, "{0}.Properties.Resources", assembly.GetName().Name);
            m_ResourceManager = assembly.GetManifestResourceNames().Any(o => o.StartsWith(resourceBaseName))
                                    ? new ResourceManager(resourceBaseName, enumType.Assembly)
                                    : null;
        }

        /// <summary>
        ///     Return the text to display for a simple value in the given culture.
        /// </summary>
        /// <param name="value">The <c>enum</c> value to get the text for.</param>
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
        ///     Convert string values to <c>enum</c> values.
        /// </summary>
        /// <param name="context">The given context.</param>
        /// <param name="culture">The given culture.</param>
        /// <param name="value">The given value.</param>
        /// <returns>The <c>enum</c> value corresponding to <paramref name="value" />.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return null;
        }

        /// <summary>
        ///     Convert the <c>enum</c> value to a string.
        /// </summary>
        /// <param name="context">The given context.</param>
        /// <param name="ci">The given culture.</param>
        /// <param name="value">The given <c>enum</c> value.</param>
        /// <param name="destinationType">The given destination <c>enum</c> type.</param>
        /// <returns>A string corresponding to the given <paramref name="value" />.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo ci, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return GetLocalizedValueText(value, ci);
            }

            return base.ConvertTo(context, ci, value, destinationType);
        }

        /// <summary>
        ///     Gets a value indicating whether this converter can convert an object in the given source type to an enumeration
        ///     object using the specified context.
        /// </summary>
        /// <returns>
        ///     true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
        /// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from. </param>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return false;
        }
    }
}