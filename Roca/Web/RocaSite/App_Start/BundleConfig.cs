using System.Web;
using System.Web.Optimization;

namespace RocaSite
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/json").Include(
                        "~/Scripts/json2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/slick").Include(
            //            "~/Scripts/jquery.event.drag.js",
            //            "~/Scripts/SlickGrid/slick*",
            //            "~/Scripts/SlickGrid/Plugins/slick*",
            //            "~/Scripts/SlickGrid/Controls/slick*"
            //            ));

            bundles.Add(new ScriptBundle("~/bundles/roca").IncludeDirectory("~/ScriptsApp/", "*.js",true));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/angular-sanitize.js",
                        //"~/Scripts/angular-resource.js",
                        //"~/Scripts/angular-route.js",
                        "~/Scripts/angular-ui-router.js",
                        "~/Scripts/angular-ui/ui-bootstrap.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                        "~/Scripts/ng-grid.js",
                        "~/Scripts/ng-grid-layout.js",
                        "~/Scripts/ng-grid-csv-export.js",
                        "~/Scripts/ng-grid-flexible-height.js",
                        "~/Scripts/angular-ui/ui-utils.js",
                        "~/Scripts/angular-ui/ui-utils-ieshiv.js",
                        "~/Scripts/smart-table.js"
                        ));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css", "~/Content/Materials.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/ng-grid.css"
                        ));

            //bundles.Add(new StyleBundle("~/Content/slick/css").Include("~/Content/slick*"));
        }
    }
}