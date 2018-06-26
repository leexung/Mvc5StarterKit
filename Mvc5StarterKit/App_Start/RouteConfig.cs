using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc5StarterKit
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //<izendaIntegration> Unqiue to Deployment Mode 3</izendaIntegration><summary>The Izenda Prefix is a string that that will be used to refer to the prefix of "Izenda Routes" for requests to the Izenda API Resources. This is initially set in the Web.Config as "api" </summary>
            var izendaApiPrefix = System.Configuration.ConfigurationManager.AppSettings["izendaapiprefix"];

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            //<izendaIntegration> Required, All Deployment Modes </izendaIntegration> <summary>Rule to establish the controller used for Izenda Exports.</summary>
            routes.MapRoute(
                name: "ReportPart",
                url: "viewer/reportpart/{id}",
                defaults: new { controller = "Home", action = "ReportPart" }
            );
            //<izendaIntegration> Optional</izendaIntegration><summary>Rule to showcase sample integration of Report Viewer</summary>
            routes.MapRoute(
                name: "ReportViewer",
                url: "report/view/{id}",
                defaults: new { controller = "Report", action = "ReportViewer" }
            );
            //<izendaIntegration> Optional</izendaIntegration><summary>Rule to showcase sample integration of Dashboard Viewer</summary>
            routes.MapRoute(
                name: "DashboardViewer",
                url: "dashboard/edit/{id}",
                defaults: new { controller = "Dashboard", action = "DashboardViewer" }
            );
            //<izendaIntegration> Optional</izendaIntegration><izendaIntegration>Optional, All Deployment Modes</izendaIntegration><summary>Rule to showcase authentication for Copy Console.</summary>
            routes.MapRoute(
               name: "CustomAuth",
               url: $"{izendaApiPrefix}/user/login",
               defaults: new { controller = "Home", action = "CustomAuth" }
           );
            //<izendaIntegration> Required, Unique to Deployment Mode 3</izendaIntegration><summary>Ensures Host App does not interfere with requests intended for Izenda API</summary>
            routes.IgnoreRoute($"{izendaApiPrefix}/{{*pathInfo}}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
