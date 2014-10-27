using System.Web.Mvc;
using System.Web.Optimization;

namespace JuicesMvc.Areas.Admin {
	public class AdminAreaRegistration : AreaRegistration {
		public override string AreaName {
			get {
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {
			context.MapRoute(
				"Admin_default",
				"Admin/{controller}/{action}/{id}",
				new { action = "Index", id = UrlParameter.Optional }
			);

			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}