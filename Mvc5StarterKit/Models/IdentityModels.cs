using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Mvc5StarterKit.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int Tenant_Id { get; set; }
        [ForeignKey("Tenant_Id")]
        public Tenant Tenant { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            if (Tenant != null)
            {
                userIdentity.AddClaims(new[] {
                    new Claim("tenantName",Tenant.Name),
                    new Claim("tenantId",Tenant.Id.ToString()),
                });
            }
            var role = (await manager.GetRolesAsync(this.Id)).FirstOrDefault();
            userIdentity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));

            // Add custom user claims here
            return userIdentity;
        }
    }

    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Tenant> Tenants { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}