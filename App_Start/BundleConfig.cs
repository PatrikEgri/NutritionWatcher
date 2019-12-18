using System.Web;
using System.Web.Optimization;

namespace NutritionWatcher
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // My style- and scriptbundles
            bundles.Add(new StyleBundle("~/BeforeLogin/styles").Include(
                      "~/Content/navbar.css",
                      "~/Content/before-login-box.css",
                      "~/Content/input-style.css"));

            bundles.Add(new ScriptBundle("~/BeforeLogin/scripts").Include(
                      "~/Content/navbar.js"));

            bundles.Add(new StyleBundle("~/AfterLogin/styles").Include(
                      "~/Content/navbar.css",
                      "~/Content/input-style.css",
                      "~/Content/table-style.css",
                      "~/Content/content.css",
                      "~/Content/dropdown.css"));

            bundles.Add(new StyleBundle("~/AfterLogin/darkstyles").Include(
                      "~/Content/DarkStyle/navbar.css",
                      "~/Content/DarkStyle/input-style.css",
                      "~/Content/DarkStyle/table-style.css",
                      "~/Content/DarkStyle/content.css",
                      "~/Content/DarkStyle/dropdown.css"));
        }
    }
}
