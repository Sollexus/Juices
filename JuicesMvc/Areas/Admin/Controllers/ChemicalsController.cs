using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Juices.DAL;
using Juices.DAL.Entities.Product;

namespace JuicesMvc.Areas.Admin.Controllers {
	public class ChemicalsController : ProductsControllerBase {
		public async Task<ActionResult> Index() {
			return View(await Context.Chemicals.ToListAsync());
		}

		public ActionResult Search(string q) {
			return Json(Context.Chemicals.Where(chem => chem.Name.Contains(q)), JsonRequestBehavior.AllowGet);
		}

		private async Task<int> Create(Chemical chemical) {
			Context.Chemicals.Add(chemical);
			await Context.SaveChangesAsync();
			return chemical.Id;
		}

		[HttpPost]
		/*[ValidateAntiForgeryToken]*/
		public async Task<ActionResult> Edit(Chemical chemical) {
			if (!ModelState.IsValid) return JsonAffirmation(chemical);

			if (chemical.Id == -1)
				return Json(await Create(chemical));

			Context.Entry(chemical).State = EntityState.Modified;
			await Context.SaveChangesAsync();
			return JsonAffirmation();
		}

		/*[ValidateAntiForgeryToken]*/
		public async Task<ActionResult> Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var chemical = await Context.Chemicals.FindAsync(id);
			if (chemical == null) {
				return HttpNotFound();
			}
			Context.Chemicals.Remove(chemical);
			await Context.SaveChangesAsync();
			return Json(true, JsonRequestBehavior.AllowGet);
		}
	}
}
