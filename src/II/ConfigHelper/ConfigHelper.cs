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
using System.Configuration;
using System.Globalization;

namespace PPWCode.Util.OddsAndEnds.II.ConfigHelper
{
    public static class ConfigHelper
    {
        public static T GetAppSetting<T>(string key, T defaultValue = default(T))
            where T : IConvertible
        {
            T result;
            try
            {
                result = ConfigurationManager.AppSettings.Get(key) != null
                             ? (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T), CultureInfo.InvariantCulture)
                             : defaultValue;
            }
            catch
            {
                result = defaultValue;
            }

            return result;
        }

        public static T GetAppSetting<T>(Configuration configuration, string key, T defaultValue = default(T))
            where T : IConvertible
        {
            T result;
            try
            {
                result = configuration.AppSettings.Settings[key] != null
                             ? (T)Convert.ChangeType(configuration.AppSettings.Settings[key].Value, typeof(T), CultureInfo.InvariantCulture)
                             : defaultValue;
            }
            catch
            {
                result = defaultValue;
            }

            return result;
        }

        public static string GetConnectionString(string key)
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings[key];
            return connectionString == null ? null : connectionString.ConnectionString;
        }
    }
}