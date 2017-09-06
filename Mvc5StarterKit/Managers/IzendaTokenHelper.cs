using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc5StarterKit.IzendaBoundary;
using Mvc5StarterKit.Models;

namespace Mvc5StarterKit.Managers
{
    public class IzendaTokenHelper
    {
        /// <summary>
        /// Get user/pwd and tenant info from web config file to authorize with Izenda Api
        /// In all (backend and front end) are integrated mode, authentication information will get from hosting web and send to izenda to authenticate.
        /// In standalone mode, hosting app will need to send user/pwd to izenda to authenticate.
        /// </summary>
        /// <returns></returns>
        public static string GetIzendaToken()
        {
            var username = System.Configuration.ConfigurationManager.AppSettings["izusername"];
            var tenantUniqueName = System.Configuration.ConfigurationManager.AppSettings["iztenantuniquename"];
            if (string.IsNullOrEmpty(tenantUniqueName))
            {
                tenantUniqueName = "System";
            }
            var token = IzendaTokenAuthorization.GetToken(new UserInfo { UserName = username, TenantUniqueName = tenantUniqueName });
            return token;
        }
    }
}