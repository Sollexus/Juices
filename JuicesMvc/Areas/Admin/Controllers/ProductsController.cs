using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JuicesMvc.Models;

namespace JuicesMvc.Areas.Admin.Controllers
{
    public class ProductsController : ProductsControllerBase
    {
		public ActionResult Index() {
			return View(Context.Products.ToList());
		}

		private int Create(Product product) {
			Context.Products.Add(product);
			Context.SaveChanges();
			return product.Id;
		}

		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		/*[ValidateAntiForgeryToken]*/
		public ActionResult Edit(Product product) {
			if (!ModelState.IsValid) return JsonAffirmation(product);

			if (product.Id == -1)
				return Json(Create(product));

			Context.Entry(product).State = EntityState.Modified;
			Context.SaveChanges();
			return JsonAffirmation();
		}

		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var product = Context.Products.Find(id);
			if (product == null) {
				return HttpNotFound();
			}
			Context.Products.Remove(product);
			Context.SaveChanges();
			return Json(true, JsonRequestBehavior.AllowGet);
		}
    }
}
