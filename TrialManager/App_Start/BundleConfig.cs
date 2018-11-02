using System.Web;
using System.Web.Optimization;

namespace TrialManager
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/dataTables.js",
                        "~/Scripts/select2.full.js",
                        "~/Scripts/icheck.js",
                        "~/scripts/bootstrap-datepicker.js",
                        "~/scripts/jquery.unobtrusive-ajax.js",
                        "~/scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/js.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
             "~/admin-lte/js/app.js",
             "~/admin-lte/plugins/fastclick/fastclick.js",
             "~/admin-lte/js/adminlte.js",
             "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/dataTables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      //"~/content/bootstrap-theme.css",
                      "~/Content/Site.css",
                      "~/Content/font-awesome.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/Content/css/bootstrap-datepicker.css",
                      "~/admin-lte/css/skins/skin-blue.css",
                      "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/datatables.css",
                      "~/Content/select2.css",
                      "~/admin-lte/css/alt/AdminLTE-select2.css",
                      "~/Content/all.css"));
        }
    }
}
