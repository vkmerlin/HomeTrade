﻿using System.Web;
using System.Web.Optimization;

namespace HRPortal.Web
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/chosen.css",
                      "~/Content/icomoon.css",
                      "~/Content/owl.transitions.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout-3.4.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/dal").Include(
            "~/Scripts/blockUI.js",
            "~/Scripts/dal.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
            "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
            "~/Scripts/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/linqJS").Include(
            "~/Scripts/linq.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockoutvalidation").Include(
            "~/Scripts/knockout.validation.js"));
        }
    }
}
