using System.Web;
using System.Web.Optimization;

namespace TrialManager
{
    public class BundleConfig
    {

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/dataTables.js",
                        "~/Scripts/select2.full.js",
                        "~/Scripts/icheck.js",
                        "~/scripts/bootstrap-datepicker.js",
                        "~/scripts/jquery.unobtrusive-ajax.js",
                        "~/scripts/moment.js",
                        "~/Scripts/js.cookie.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/admin-lte/js/scripts").Include(
             "~/admin-lte/js/app.js",
             "~/admin-lte/js/adminlte.js"));

            bundles.Add(new ScriptBundle("~/admin-lte/plugins/scripts").Include(
             "~/admin-lte/plugins/fastclick/fastclick.js",
             "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Scripts/modernizr/script").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/dataTables.bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                      "~/Content/bootstrap.css",
                      //"~/content/bootstrap-theme.css",
                      "~/Content/Site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/css/bootstrap-datepicker.css",
                      "~/Content/datatables.css",
                      "~/Content/select2.css",
                      "~/Content/all.css"));

            bundles.Add( new StyleBundle("~/admin-lte/css/styles").Include(
                      "~/admin-lte/css/alt/AdminLTE-select2.css",
                      "~/admin-lte/css/AdminLTE.css",
                      "~/admin-lte/css/skins/skin-blue.css"
                ));
            bundles.Add(new StyleBundle("~/admin-lte/plugins/style").Include(
                      "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
                ));
        }
    }
}
