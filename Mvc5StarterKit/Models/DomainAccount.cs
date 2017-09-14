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
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}