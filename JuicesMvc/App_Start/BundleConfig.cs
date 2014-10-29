using System.Web;
using System.Web.Optimization;

namespace JuicesMvc {
	public class BundleConfig {
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles) {
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
					  "~/Scripts/respond.js",
					  "~/Scripts/bootbox.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
					  "~/Content/bootstrap-tokeninput.css"
					  ));

			bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
						"~/Scripts/knockout-{version}.js",
						"~/Scripts/knockout.mapping-latest.js"
			));

			bundles.Add(new ScriptBundle("~/bundles/jqueryExtensions").Include(
						"~/Scripts/jquery.tokeninput.js"));

			bundles.Add(new ScriptBundle("~/bundles/knockoutExtensions").Include(
						"~/Scripts/MyScripts/knockout/knockout.tokeninput.js",
						"~/Scripts/MyScripts/knockout/protectedObservables.js",
						"~/Scripts/MyScripts/knockout/protectedArrayObservables.js",
						"~/Scripts/MyScripts/knockout/aspNetValidation.js"));

			bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
						"~/Scripts/cb-ko-binding-chosen.js",
						"~/Scripts/chosen-jquery.js"
			));


			bundles.Add(new ScriptBundle("~/bundles/myScripts").Include(
						"~/Scripts/MyScripts/common.js"
			));

			// Set EnableOptimizations to false for debugging. For more information,
			// visit http://go.microsoft.com/fwlink/?LinkId=301862
			BundleTable.EnableOptimizations = false;
		}
	}
}
