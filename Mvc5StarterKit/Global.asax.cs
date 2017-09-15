using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Mvc5StarterKit
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.LogManager.GetLogger(typeof(MvcApplication)).Debug("Starter Kit is starting up...");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IzendaConfig.RegisterLoginLogic();
            
            // override AntiForgery configuration for application ClaimIdentity
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimsIdentity.DefaultNameClaimType;
            log4net.LogManager.GetLogger(typeof(MvcApplication)).Debug("Starter Kit has started up successfully.");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            log4net.LogManager.GetLogger(typeof(MvcApplication)).Error("Unhandled error", exception);
            Server.ClearError();
        }
    }
}
