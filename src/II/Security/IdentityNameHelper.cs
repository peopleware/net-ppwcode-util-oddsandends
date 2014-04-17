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

using System.Security.Principal;
using System.ServiceModel;
using System.Threading;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.Security
{
    public static class IdentityNameHelper
    {
        #region GetIdentityName

        public static string GetServiceSecurityContextIdentityName()
        {
            return ServiceSecurityContext.Current != null
                   && !string.IsNullOrEmpty(ServiceSecurityContext.Current.WindowsIdentity.Name)
                       ? ServiceSecurityContext.Current.WindowsIdentity.Name
                       : null;
        }

        public static string GetThreadCurrentPrincipalIdentityName()
        {
            return Thread.CurrentPrincipal != null
                   && !string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name)
                       ? Thread.CurrentPrincipal.Identity.Name
                       : null;
        }

        public static string GetWindowsIdentityIdentityName()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            if (current == null)
            {
                return null;
            }
            WindowsPrincipal wp = new WindowsPrincipal(current);
            return !string.IsNullOrEmpty(wp.Identity.Name) ? wp.Identity.Name : null;
        }

        public static string GetIdentityName()
        {
            return
                GetThreadCurrentPrincipalIdentityName() ??
                GetServiceSecurityContextIdentityName() ??
                GetWindowsIdentityIdentityName() ??
                "Unknown";
        }

        #endregion
    }
}
