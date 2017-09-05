using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Mvc5StarterKit.Managers
{
    public class IzendaSkinHelper
    {
        public static string GetCurrentTenantCssPath()
        {
            //Using tenantId, identify location of folder where the css resides for the given tenant and build path to CSS.
            var currentTenant = HttpContext.Current.User.Identity != null ? ((ClaimsIdentity)HttpContext.Current.User.Identity).FindFirstValue("tenantName") : null;
            var izendaCssPath = currentTenant == null ? "" :
                $"/Content/{currentTenant.ToUpper()}/izenda-{currentTenant.ToLower()}.css";

            if (!string.IsNullOrWhiteSpace(izendaCssPath))
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath(izendaCssPath)))
                {
                    izendaCssPath = string.Empty;
                }
            }

            return izendaCssPath;
        }

        public static string GetCurrentProductVersion()
        {
            const string izendApiLib = "/bin/Izenda.BI.API.dll";
            return FileVersionInfo.GetVersionInfo(HttpContext.Current.Server.MapPath(izendApiLib)).ProductVersion;
        }
    }
}