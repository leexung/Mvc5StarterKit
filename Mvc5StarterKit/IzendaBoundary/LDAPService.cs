using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;
using Mvc5StarterKit.Models;

namespace Mvc5StarterKit.IzendaBoundary
{
    public sealed class LDAPService
    {
        #region Singleton
        static private readonly LDAPService Instance = new LDAPService();

        private LDAPService()
        {

        }
        public static LDAPService GetInstance()
        {
            return Instance;
        }
        #endregion

        /// <summary>
        /// This method doesnot need an account to connect to AD to setup a PrincipalContext
        /// Can use this method for a secure LDAP connection (LDAPS)
        /// Secure LDAP port: 636, Non-secure LDAP port: 389
        /// </summary>
        public bool Authenticate(string username, string password)
        {
            var domain = ConfigurationManager.AppSettings["LDAPName"];
            var port = ConfigurationManager.AppSettings["LDAPPort"];

            try
            {
                using (var ldapConnection = new LdapConnection(domain + ":" + port))
                {
                    var networkCredential = new NetworkCredential(username, password, domain);
                    ldapConnection.SessionOptions.SecureSocketLayer = false;
                    ldapConnection.AuthType = AuthType.Negotiate;
                    ldapConnection.Bind(networkCredential);
                }

                // if the bind succeeds, the credentials are OK
                return true;
            }
            catch (LdapException ex)
            {
                return false;
            }
        }

        private static PrincipalContext CreateDomainContext()
        {
            var domain = ConfigurationManager.AppSettings["LDAPName"];
            var username = ConfigurationManager.AppSettings["LDAPUserName"];
            var password = ConfigurationManager.AppSettings["LDAPPassword"];

            return new PrincipalContext(ContextType.Domain, domain, username, password);
        }

        /// <summary>
        /// Get all groups in Active Directory
        /// </summary>
        /// <returns></returns>
        public IList<ADGroup> GetADGroupsAsync()
        {
            var listGroupResult = new List<ADGroup>();
            // Create domain context
            using (var domainContext = CreateDomainContext())
            {
                // Create principal searcher passing in the GroupPrincipal
                using (var searcher = new PrincipalSearcher(new GroupPrincipal(domainContext)))
                {
                    foreach (var found in searcher.FindAll())
                    {
                        var group = found as GroupPrincipal;
                        if (group != null)
                        {
                            listGroupResult.Add(new ADGroup
                            {
                                Name = group.Name,
                                SamAccountName = group.SamAccountName,
                                DistinguishedName = group.DistinguishedName,
                                Description = group.Description
                            });
                        }
                    }
                }
            }
            return listGroupResult;
        }

        /// <summary>
        /// Get all users in AD
        /// </summary>
        /// <returns></returns>
        public IList<ADUser> GetADUsers()
        {
            var listUserResult = new List<ADUser>();
            // Create domain context
            using (var domainContext = CreateDomainContext())
            {
                // Create principal searcher passing in the UserPrincipal
                using (var searcher = new PrincipalSearcher(new UserPrincipal(domainContext)))
                {
                    foreach (var found in searcher.FindAll())
                    {
                        var dirEntry = found.GetUnderlyingObject() as DirectoryEntry;
                        if (dirEntry != null)
                        {
                            listUserResult.Add(ADUser.Create(dirEntry));
                        }
                    }
                }
            }
            return listUserResult;
        }

        /// <summary>
        /// Get AD User details
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public ADUser GetADUserDetail(string samAccountName)
        {
            using (var domainContext = CreateDomainContext())
            {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, samAccountName))
                {
                    // Get user detail info
                    var dirEntry = foundUser?.GetUnderlyingObject() as DirectoryEntry;
                    if (dirEntry == null)
                    {
                        return null;
                    }

                    var adUser = ADUser.Create(dirEntry);

                    // Get user's group
                    var principalSearchResult = foundUser.GetAuthorizationGroups();
                    // iterate over all groups
                    foreach (var principal in principalSearchResult)
                    {
                        // make sure to add only group principals
                        if (principal is GroupPrincipal)
                        {
                            adUser.Groups.Add(new ADGroup
                            {
                                Name = principal.Name,
                                SamAccountName = principal.SamAccountName,
                                DistinguishedName = principal.DistinguishedName,
                                Description = principal.Description
                            });
                        }
                    }

                    return adUser;
                }
            }
        }
    }
}