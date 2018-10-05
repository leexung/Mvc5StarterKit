using System.Web;
using System.Web.Optimization;

namespace Mvc5StarterKit
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // #izenda
            bundles.Add(new ScriptBundle("~/bundles/izenda").Include(
                       "~/Scripts/izenda/izenda_common.js",
                       "~/Scripts/izenda/izenda_locales.js",
                       "~/Scripts/izenda/izenda_vendors.js",
                       "~/Scripts/izenda/izenda_ui.js",
                       "~/Scripts/izenda.integrate.js",
                       "~/Scripts/izenda.utils.js",
                       "~/Scripts/izenda/customchart.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/alertify.css", // #izenda
                      "~/Content/site.css"));
        }
    }
}
