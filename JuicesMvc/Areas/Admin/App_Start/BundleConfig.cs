using System.Web.Optimization;

namespace JuicesMvc.Areas.Admin {
	internal static class BundleConfig {
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.Add(new ScriptBundle("~/admin/bundles/common").Include(
						"~/Areas/Admin/Scripts/entitiesEditor.js"));
		}
	}
}
