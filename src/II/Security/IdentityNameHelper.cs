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

using System.Security.Principal;
using System.ServiceModel;
using System.Threading;

namespace PPWCode.Util.OddsAndEnds.II.Security
{
    /// <summary>
    /// Helper class for IdentityName.
    /// </summary>
    public static class IdentityNameHelper
    {
        /// <summary>
        /// Gets the identity name of the security context of a remote party.
        /// </summary>
        /// <returns>The identity name.</returns>
        public static string GetServiceSecurityContextIdentityName()
        {
            return ServiceSecurityContext.Current != null
                   && !string.IsNullOrEmpty(ServiceSecurityContext.Current.WindowsIdentity.Name)
                       ? ServiceSecurityContext.Current.WindowsIdentity.Name
                       : null;
        }

        /// <summary>
        /// Gets the identity name of the current principal.
        /// </summary>
        /// <returns>The identity name.</returns>
        public static string GetThreadCurrentPrincipalIdentityName()
        {
            return Thread.CurrentPrincipal != null
                   && !string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name)
                       ? Thread.CurrentPrincipal.Identity.Name
                       : null;
        }

        /// <summary>
        /// Gets the name of the current windows user.
        /// </summary>
        /// <returns>The name of the current windows user.</returns>
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

        /// <summary>
        /// Gets the identity name.
        /// </summary>
        /// <returns>The identity name.</returns>
        public static string GetIdentityName()
        {
            return
                GetThreadCurrentPrincipalIdentityName() ??
                GetServiceSecurityContextIdentityName() ??
                GetWindowsIdentityIdentityName() ??
                "Unknown";
        }
    }
}