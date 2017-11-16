using System.Web;
using System.Web.Optimization;

namespace CuciMobilv4
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/front/bower_components/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/front/bower_components/jquery-ui/jquery-ui.min.js",
                      "~/front/bower_components/bootstrap/dist/js/bootstrap.min.js",
                      "~/front/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/front/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/front/bower_components/fastclick/lib/fastclick.js",
                      "~/front/dist/js/adminlte.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/front/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/front/bower_components/font-awesome/css/font-awesome.min.css",
                      "~/front/bower_components/Ionicons/css/ionicons.min.css",
                      "~/front/dist/css/AdminLTE.min.css",
                      "~/front/dist/css/skins/_all-skins.min.css",
                      "~/front/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
                      ));
        }
    }
}
