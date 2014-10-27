using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JuicesMvc.Models;

namespace JuicesMvc.Areas.Admin.Controllers {
	public class TechnologiesController : ProductsControllerBase {
		public ActionResult Index() {
			return View(Context.Technologies.ToList());
		}

		private int Create(Technology technology) {
			Context.Technologies.Add(technology);
			Context.SaveChanges();
			return technology.Id;
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		/*[ValidateAntiForgeryToken]*/
		public ActionResult Edit(Technology technology) {
			if (!ModelState.IsValid) return JsonAffirmation(technology);

			if (technology.Id == -1)
				return Json(Create(technology));

			Context.Entry(technology).State = EntityState.Modified;
			Context.SaveChanges();
			return JsonAffirmation();
		}

		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var tech = Context.Technologies.Find(id);
			if (tech == null) {
				return HttpNotFound();
			}
			Context.Technologies.Remove(tech);
			Context.SaveChanges();
			return Json(true, JsonRequestBehavior.AllowGet);
		}
	}
}
