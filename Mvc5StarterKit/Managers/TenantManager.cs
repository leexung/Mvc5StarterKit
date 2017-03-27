using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Mvc5StarterKit.Managers
{
    public class TenantManager
    {
        public Models.Tenant GetTenantByName(string name)
        {
            using (var context = Models.ApplicationDbContext.Create())
            {
                var tenant = context.Tenants.Where(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();

                return tenant;
            }
        }


        public async Task<Models.Tenant> SaveTenantAsync(Models.Tenant tenant)
        {
            using (var context = Models.ApplicationDbContext.Create())
            {
                context.Tenants.Add(tenant);
                await context.SaveChangesAsync();

                return tenant;
            }
        }
    }
}