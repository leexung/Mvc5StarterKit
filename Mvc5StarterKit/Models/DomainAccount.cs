using System.ComponentModel.DataAnnotations;

namespace Mvc5StarterKit.Models
{
    public class DomainAccount
    {
        [Required]
        [Display(Name = "Domain")]
        public string DomainName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public QueryType QueryType { get; set; }
    }

    public enum QueryType
    {
        SaveTenant = 0,

        Group = 1,

        User = 2
    }
}