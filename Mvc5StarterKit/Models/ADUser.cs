using System.Collections.Generic;
using System.DirectoryServices;

namespace Mvc5StarterKit.Models
{
    public class ADUser
    {
        public string DomainName { get; set; }

        public string SamAccountName { get; set; }

        public string UserPrincipalName { get; set; }

        public string DistinguishedName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Description { get; set; }

        #region Iz User Info

        public bool IsSystemAdmin { get; set; }

        #endregion

        public IList<ADGroup> Groups { get; set; }

        public ADUser()
        {
            this.Groups = new List<ADGroup>();
        }

        public static ADUser Create(DirectoryEntry dirEntry)
        {
            return new ADUser
            {
                SamAccountName = dirEntry.Properties["samAccountName"].Value + string.Empty,
                UserPrincipalName = dirEntry.Properties["userPrincipalName"].Value + string.Empty,
                DistinguishedName = dirEntry.Properties["distinguishedName"].Value + string.Empty,
                Description = dirEntry.Properties["description"].Value + string.Empty,
                FirstName = dirEntry.Properties["givenName"].Value + string.Empty,
                LastName = dirEntry.Properties["sn"].Value + string.Empty,
            };
        }
    }
}