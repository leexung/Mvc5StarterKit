using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Izenda.BI.Framework.Models;
using Izenda.BI.Framework.Models.DBStructure;
using Mvc5StarterKit.Models;
using Izenda.BI.Logic.CustomConfiguration;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Mvc5StarterKit.IzendaBoundary;
using Mvc5StarterKit.Managers;

namespace Mvc5StarterKit.Controllers
{
    public class ADSyncController : Controller
    {
        protected static ILog Logger = LogManager.GetLogger(typeof(ADSyncController));

        #region Dependency Properties

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        private RoleManager<IdentityRole> _roleManager;

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                if (_roleManager != null)
                {
                    return _roleManager;
                }

                var appContext = new ApplicationDbContext();
                _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(appContext));
                return _roleManager;
            }
            private set
            {
                _roleManager = value;
            }
        }

        #endregion

        // GET: ADSync
        public async Task<ActionResult> Index()
        {
            var account = GetConfiguredDomainAccount();
            // Check account authenticate
            if (!LDAPService.GetInstance().Authenticate(account.Username, account.Password))
            {
                ViewBag.AuthenticatedFailMessage =
                    $"Cannot authenticate with your configured domain account {account.DomainName}\\{account.Username}. Check your configured account settings in Web.config";
                return View(account);
            }
            ViewBag.IsValidLDAPConfig = true;

            // Check izenda tenant is existing or not
            var isExistIzendaTenant = await CheckIsExistingIzendaTenant(account.DomainName);
            ViewBag.IzendaTenantWasGenerated = isExistIzendaTenant;
            if (isExistIzendaTenant)
            {
                // Make sure tenant also is existing in integrated DB
                await AddIntegratedTenantIfNotExisting(account.DomainName);
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(DomainAccount account)
        {
            // Check account authenticate
            if (!LDAPService.GetInstance().Authenticate(account.Username, account.Password))
            {
                ViewBag.AuthenticatedFailMessage =
                    $"Cannot authenticate with your configured domain account {account.DomainName}\\{account.Username}. Check your configured account settings in Web.config.";
                return View(account);
            }
            ViewBag.IsValidLDAPConfig = true;

            // Generate integrated tenant
            await AddIntegratedTenantIfNotExisting(account.DomainName);

            // Generate izenda tenant
            CreateIzendaTenant(account.DomainName);
            return View(account);
        }

        /// <summary>
        /// Check that whether existing tenant in Izenda DB or not
        /// </summary>
        /// <param name="tenantName">The tenant name</param>
        /// <returns>True if tenant is existing, otherwise false</returns>
        private async Task<bool> CheckIsExistingIzendaTenant(string tenantName)
        {
            var izendaToken = IzendaTokenHelper.GetIzendaToken();
            var allIzendaTenants = await IzendaUtility.GetTenants(izendaToken);
            return allIzendaTenants.Any(t => t.Name.Equals(tenantName));
        }

        private void CreateIzendaTenant(string tenantName)
        {
            var izendaTenant = new Tenants
            {
                Active = true,
                Deleted = false,
                Name = tenantName,
                TenantID = tenantName,
                Description = $"Generated from AD domain {tenantName}",
                Permission = Izenda.BI.Framework.Utility.PermissionUtil.FullAccess
            };

            TenantIntegrationConfig.AddOrUpdateTenant(izendaTenant);
        }

        private static DomainAccount GetConfiguredDomainAccount()
        {
            var domainAccount = new DomainAccount
            {
                DomainName = ConfigurationManager.AppSettings["LDAPName"],
                Username = ConfigurationManager.AppSettings["LDAPUserName"],
                Password = ConfigurationManager.AppSettings["LDAPPassword"]
            };
            return domainAccount;
        }

        #region AD Groups

        public ActionResult ADGroups()
        {
            var account = GetConfiguredDomainAccount();
            // Check account authenticate
            if (!LDAPService.GetInstance().Authenticate(account.Username, account.Password))
            {
                return RedirectToAction("Index");
            }

            // Keep domain name on view for post action
            ViewBag.DomainName = account.DomainName;

            // Query all AD groups
            var listGroups = LDAPService.GetInstance().GetADGroupsAsync();

            return View(listGroups);
        }

        /// <summary>
        /// Save AD group as role in izenda db
        /// </summary>
        /// <param name="tenantUniqueName">Tenant name</param>
        /// <param name="roleName">Goup/Role name</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SaveIzendaRole(string tenantUniqueName, string roleName)
        {
            if (!RoleManager.RoleExists(roleName))
            {
                var identityResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                if (!identityResult.Succeeded)
                {
                    Logger.ErrorFormat("Failed to insert integrated role [{0}]", roleName);
                }
            }
            try
            {
                var izendaToken = IzendaTokenHelper.GetIzendaToken();
                var allIzendaTenants = await IzendaUtility.GetTenants(izendaToken);
                var domainTenant = allIzendaTenants.Single(t => t.Name.Equals(tenantUniqueName));
                var allRoles = await IzendaUtility.GetRoles(domainTenant.Id, izendaToken);
                if (!allRoles.Any(t => t.Name.Equals(roleName)))
                {
                    Logger.DebugFormat("Saved AD Group [{0}] as a tenant in Izenda", roleName);
                    CreateIzendaRole(tenantUniqueName, roleName);
                }
               
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to save role [{roleName}]", ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region AD Users

        public ActionResult ADUsers()
        {
            var account = GetConfiguredDomainAccount();
            // Check account authenticate
            if (!LDAPService.GetInstance().Authenticate(account.Username, account.Password))
            {
                return RedirectToAction("Index");
            }

            ViewBag.DomainName = account.DomainName;

            // Query all AD users
            var listUsers = LDAPService.GetInstance().GetADUsers();

            return View(listUsers);
        }

        public ActionResult ADUserDetail(string samAccountName)
        {
            var adUser = LDAPService.GetInstance().GetADUserDetail(samAccountName);
            adUser.DomainName = ConfigurationManager.AppSettings["LDAPName"];
            return View(adUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveIzendaUser(ADUser adUser)
        {
            if (ModelState.IsValid)
            {
                // Save domain as tenant in integrated db
                var tenant = await AddIntegratedTenantIfNotExisting(adUser.DomainName);

                // Create user in integrated db
                var result = await CreateIntegratedRolesAndUser(adUser, tenant);
                if (result.Succeeded)
                {
                    // Create izenda user
                    await CreateIzendaUser(adUser, tenant.Name);
                }
                else
                {
                    AddIdentityResultErrors(result);
                }

                return RedirectToAction("ADUsers");
            }

            return View("ADUserDetail", adUser);
        }
        
        private static async Task CreateIzendaUser(ADUser adUser, string tenant)
        {
            // Break saving flow if izenda user is already exist
            var izendaToken = IzendaTokenHelper.GetIzendaToken();
            var allIzendaTenants = await IzendaUtility.GetTenants(izendaToken);
            var domainTenant = allIzendaTenants.Single(t => t.Name.Equals(tenant));
            var allTenantUsers = await IzendaUtility.GetUsers(domainTenant.Id, izendaToken);
            if (allTenantUsers.Any(u => u.UserName.Equals(adUser.SamAccountName)))
            {
                Logger.WarnFormat("Izenda user {0} is alredy exist", adUser.SamAccountName);
                return;
            }

            var izendaUser = new UserDetail()
            {
                UserName = adUser.SamAccountName,
                EmailAddress = adUser.UserPrincipalName,
                FirstName = adUser.FirstName,
                LastName = adUser.LastName,
                TenantDisplayId = adUser.DomainName,
                SystemAdmin = false,//Tenant user never is system admin
                Deleted = false,
                Active = true,
                Roles = new List<Role>()
            };

            var allRoles = await IzendaUtility.GetRoles(domainTenant.Id, izendaToken);

            foreach (var adGroup in adUser.Groups)
            {
                if (adGroup.IsSelected)
                {
                    izendaUser.Roles.Add(new Role
                    {
                        Name = adGroup.Name
                    });

                    if (!allRoles.Any(r => r.Name.Equals(adGroup.Name)))
                    {
                        //determine roles
                        CreateIzendaRole(tenant, adGroup.Name);
                        Logger.InfoFormat("Save new role {0} successfully", adGroup.Name);
                    }
                }
            }

            UserIntegrationConfig.AddOrUpdateUser(izendaUser);
            Logger.InfoFormat("Save Izenda user {0} successfully", adUser.SamAccountName);
        }

        private static void CreateIzendaRole(string tenant, string roleName)
        {
            var roleDetail = new RoleDetail
            {
                Name = roleName,
                TenantUniqueName = tenant,
                Active = true,
                Permission = Izenda.BI.Framework.Utility.PermissionUtil.FullAccess
            };
            RoleIntegrationConfig.AddOrUpdateRole(roleDetail);
        }

        private async Task<IdentityResult> CreateIntegratedRolesAndUser(ADUser adUser, Tenant tenant)
        {
            // Create role in integrated db
            await CreateIntegratedRoles(adUser.Groups);

            adUser.Password = ConfigurationManager.AppSettings["DefaultADUserSyncPassword"];
            var user = new ApplicationUser
            {
                UserName = adUser.SamAccountName,
                Email = adUser.UserPrincipalName,
                Tenant_Id = tenant.Id,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await UserManager.CreateAsync(user, adUser.Password);
            
            // Genereate relationship entry between user and role
            await LinkIntegratedRoleToUser(user.Id, adUser.Groups);

            return result;
        }

        private async Task CreateIntegratedRoles(IList<ADGroup> groups)
        {
            foreach (var adGroup in groups)
            {
                if (!RoleManager.RoleExists(adGroup.Name))
                {
                    var identityResult = await RoleManager.CreateAsync(new IdentityRole(adGroup.Name));
                    if (!identityResult.Succeeded)
                    {
                        Logger.ErrorFormat("Failed to insert integrated role [{0}]", adGroup.Name);
                    }
                }
            }
        }

        private async Task LinkIntegratedRoleToUser(string userId, IList<ADGroup> roles)
        {
            //await UserManager.AddToRoleAsync(user.Id, roleName);
            foreach (var adGroup in roles)
            {
                var identityRet = await UserManager.AddToRoleAsync(userId, adGroup.Name);
                if (!identityRet.Succeeded)
                {
                    Logger.ErrorFormat("Failed to link integrated role {0} with user id {1}", adGroup.Name, userId);
                }
            }
        }

        private static async Task<Tenant> AddIntegratedTenantIfNotExisting(string tenantName)
        {
            var tenant = new Tenant { Name = tenantName };
            var tenantManager = new TenantManager();
            var exstingTenant = tenantManager.GetTenantByName(tenant.Name);

            if (exstingTenant != null)
                tenant = exstingTenant;
            else
                tenant = await tenantManager.SaveTenantAsync(tenant);
            return tenant;
        }

        #endregion

        private void AddIdentityResultErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}