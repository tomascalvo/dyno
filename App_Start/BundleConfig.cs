using System.Web.Optimization;

namespace DevPath
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/landing_page_js/jquery.min.js",
                        "~/Scripts/landing_page_js/jquery.easing.min.js",
                        "~/Scripts/landing_page_js/jquery.magnific-popup.js",
                        "~/Scripts/landing_page_js/morphext.min.js",
                        "~/Scripts/landing_page_js/swiper.min.js",
                        "~/Scripts/landing_page_js/scripts.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js"));
                      "~/Scripts/landing_page_js/bootstrap.min.js"
                      ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                    //"~/Content/bootstrap.css",
                    "~/Content/Landing_Page_Content/css/bootstrap.css",
                    "~/Content/Landing_Page_Content/css/fontawesome-all.css",
                    "~/Content/Landing_Page_Content/css/swiper.css",
                    "~/Content/Landing_Page_Content/css/magnific-popup.css",
                    "~/Content/Landing_Page_Content/css/syles.css",
                    "~/Content/Site.css"
                    ));
        }
    }
}
