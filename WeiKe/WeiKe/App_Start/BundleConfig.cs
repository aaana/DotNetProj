using System.Web;
using System.Web.Optimization;

namespace WeiKe
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/weike").Include("~/Scripts/weike.js"));

            //bundles.Add(new ScriptBundle("~/bundles/log").Include("~/Scripts/log.js"));

            bundles.Add(new ScriptBundle("~/bundles/public").Include(
                      "~/Scripts/public/classie.js",
                      "~/Scripts/public/imagesloaded.pkgd.min.js",
                      "~/Scripts/public/masonry.pkgd.min.js",
                      "~/Scripts/public/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/base/css").Include(
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/navBar.css"));

            bundles.Add(new StyleBundle("~/weike/css").Include(
                "~/Content/pbl/default.css",
                "~/Content/weike.css",
                "~/Content/sidebar.css",
                "~/Content/pblItem.css"));

            bundles.Add(new StyleBundle("~/publish/css").Include(
                "~/Content/publish.css"));

            bundles.Add(new StyleBundle("~/personalPage/css").Include(
                "~/Content/personalPage.css"));

            bundles.Add(new StyleBundle("~/message/css").Include(
                "~/Content/message.css"));

            bundles.Add(new StyleBundle("~/auth/css").Include(
                "~/Content/auth.css"));

            bundles.Add(new StyleBundle("~/playground/css").Include(
                "~/Cotent/playground.css"));
        }
    }
}
