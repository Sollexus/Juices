using Juices.DAL;
using JuicesMvc.Controllers;
using JuicesMvc.Models;

namespace JuicesMvc.Areas.Admin.Controllers {
	public class ProductsControllerBase : BaseController {
		protected readonly JuicyContext Context = new JuicyContext();

		protected override void Dispose(bool disposing) {
			if (disposing) {
				Context.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}