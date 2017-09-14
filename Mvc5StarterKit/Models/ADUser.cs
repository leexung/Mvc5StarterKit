using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.Models
{
    public class ADUser
    {
        public string DomainName { get; set; }

        public string SamAccountName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string UserPrincipalName { get; set; }

        public string DistinguishedName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Description { get; set; }

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