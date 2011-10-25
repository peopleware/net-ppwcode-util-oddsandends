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

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.DirectoryServices;
using System.Linq;
using System.Text;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.ActiveDirectory
{
    public class AdSearch
    {
        private readonly string m_DomainName;

        public string DomainName
        {
            get
            {
                return m_DomainName;
            }
        }

        public AdSearch(string domainName)
        {
            Contract.Requires(!string.IsNullOrEmpty(domainName));
            Contract.Ensures(DomainName == domainName);

            m_DomainName = domainName;
        }

        /// <summary>
        /// This function parses the domain out of an user-account.
        /// </summary>
        /// <param name="userAccount">A useraccount has following format: DOMAIN\UserName</param>
        /// <returns>The domain of an user-account</returns>
        public static string GetDomainFromUserAccount(string userAccount)
        {
            Contract.Requires(!string.IsNullOrEmpty(userAccount));
            Contract.Requires(userAccount.Length > 2);
            Contract.Requires(userAccount.ToCharArray().Where(c => c == '\\').Count() == 1);
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            StringBuilder sb = new StringBuilder(userAccount.Length);
            int i = 0;
            while (i < userAccount.Length && userAccount[i] != '\\')
            {
                sb.Append(userAccount[i++]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// This function parses the UserName out of an user-account.
        /// </summary>
        /// <param name="userAccount">A useraccount has following format: DOMAIN\UserName</param>
        /// <returns></returns>
        public static string GetAccountNameFromUserAccount(string userAccount)
        {
            Contract.Requires(!string.IsNullOrEmpty(userAccount));
            Contract.Requires(userAccount.Length > 2);
            Contract.Requires(userAccount.ToCharArray().Where(c => c == '\\').Count() == 1);
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            return userAccount.Replace(GetDomainFromUserAccount(userAccount) + @"\", string.Empty);
        }

        private DirectoryEntry GetDirectoryEntry()
        {
            DirectoryEntry de = new DirectoryEntry
            {
                Path = string.Format(@"LDAP://DC={0},DC=local", m_DomainName),
                AuthenticationType = AuthenticationTypes.Secure,
            };
            return de;
        }

        /// <summary>
        /// List all available properties of an SAM Account.
        /// </summary>
        /// <param name="userAccount">A useraccount has following format: DOMAIN\UserName</param>
        /// <returns></returns>
        public IEnumerable<string> AvailableProperties(string userAccount)
        {
            Contract.Requires(!string.IsNullOrEmpty(userAccount));
            Contract.Requires(userAccount.Length > 2);
            Contract.Requires(userAccount.ToCharArray().Where(c => c == '\\').Count() == 1);
            Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

            using (DirectoryEntry directoryEntry = GetDirectoryEntry())
            {
                string account = GetAccountNameFromUserAccount(userAccount);

                using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry))
                {
                    directorySearcher.Filter = string.Format("(SAMAccountName={0})", account);
                    SearchResult result = directorySearcher.FindOne();
                    return result != null && result.Properties != null && result.Properties.PropertyNames != null
                               ? result.Properties.PropertyNames.OfType<string>()
                               : Enumerable.Empty<string>();
                }
            }
        }

        /// <summary>
        /// Get the value for a specified property name of a SAM account
        /// </summary>
        /// <param name="userAccount">A useraccount has following format: DOMAIN\UserName</param>
        /// <param name="propertyName">A valid property name, the list of valid properties
        /// can be found using <see cref="AvailableProperties"/></param>
        /// <returns></returns>
        public ResultPropertyValueCollection GetProperty(string userAccount, string propertyName)
        {
            Contract.Requires(!string.IsNullOrEmpty(userAccount));
            Contract.Requires(userAccount.Length > 2);
            Contract.Requires(userAccount.ToCharArray().Where(c => c == '\\').Count() == 1);
            Contract.Requires(!string.IsNullOrEmpty(propertyName));

            using (DirectoryEntry directoryEntry = GetDirectoryEntry())
            {
                using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry))
                {
                    string accountName = GetAccountNameFromUserAccount(userAccount);
                    directorySearcher.Filter = string.Format("(SAMAccountName={0})", accountName);
                    directorySearcher.PropertiesToLoad.Add(propertyName);
                    SearchResult result = directorySearcher.FindOne();
                    return result != null
                               ? result.Properties[propertyName]
                               : null;
                }
            }
        }

        /// <summary>
        /// Checks if a certain user exists
        /// </summary>
        /// <param name="userAccount">A useraccount has following format: DOMAIN\UserName</param>
        /// <returns></returns>
        public bool UserExists(string userAccount)
        {
            Contract.Requires(!string.IsNullOrEmpty(userAccount));
            Contract.Requires(userAccount.Length > 2);
            Contract.Requires(userAccount.ToCharArray().Where(c => c == '\\').Count() == 1);

            using (DirectoryEntry directoryEntry = GetDirectoryEntry())
            {
                using (DirectorySearcher directorySearcher = new DirectorySearcher())
                {
                    directorySearcher.SearchRoot = directoryEntry;
                    string accountName = GetAccountNameFromUserAccount(userAccount);
                    directorySearcher.Filter = string.Format("(&(objectClass=user)(SAMAccountName={0}))", accountName);
                    SearchResultCollection results = directorySearcher.FindAll();
                    return results.Count > 0;
                }
            }
        }

        /// <summary>
        /// Get the displayname property of a SAM-account
        /// </summary>
        /// <param name="userAccount">A useraccount has following format: DOMAIN\UserName</param>
        /// <returns></returns>
        public string FindName(string userAccount)
        {
            Contract.Requires(!string.IsNullOrEmpty(userAccount));
            Contract.Requires(userAccount.Length > 2);
            Contract.Requires(userAccount.ToCharArray().Where(c => c == '\\').Count() == 1);
            Contract.Ensures(Contract.Result<string>() != null);

            ResultPropertyValueCollection result = GetProperty(userAccount, @"displayName");
            return result != null
                       ? result[0].ToString()
                       : string.Empty;
        }

        /// <summary>
        /// Get the email of a SAM-account
        /// </summary>
        /// <param name="userAccount">A useraccount has following format: DOMAIN\UserName</param>
        /// <returns></returns>
        public string FindEmail(string userAccount)
        {
            Contract.Requires(!string.IsNullOrEmpty(userAccount));
            Contract.Requires(userAccount.Length > 2);
            Contract.Requires(userAccount.ToCharArray().Where(c => c == '\\').Count() == 1);
            Contract.Ensures(Contract.Result<string>() != null);

            ResultPropertyValueCollection result = GetProperty(userAccount, @"mail");
            return result != null
                       ? result[0].ToString()
                       : string.Empty;
        }
    }
}