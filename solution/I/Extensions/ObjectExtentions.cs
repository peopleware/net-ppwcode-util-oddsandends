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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.Extensions
{
    public static class ObjectExtentions
    {
        [Pure]
        public static string ToLogString(this object obj)
        {
            if (obj != null)
            {
                StringBuilder sb = new StringBuilder(obj.ToString());
                {
                    sb.Append("{ ");
                    sb.AppendFormat("HashCode = '{0}'", obj.GetHashCode());

                    foreach (PropertyInfo prop in obj.GetType().GetProperties().Where(prop => prop.PropertyType.IsValueType))
                    {
                        object value;
                        try
                        {
                            value = prop.GetValue(obj, null);
                        }
                        catch (Exception e)
                        {
                            value = e.GetBaseException().Message;
                        }
                        sb.AppendFormat(", {0} = '{1}'", prop.Name, value);
                    }
                    sb.Append(" }");
                }
                return sb.ToString();
            }

            return null;
        }
    }
}
