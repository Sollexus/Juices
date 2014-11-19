using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Juices.DAL.Entities.Product;
using JuicesMvc.Dtos.Products;
using JuicesMvc.Models.Products;

namespace JuicesMvc.Areas.Admin.Controllers {
	public class ProductsController : ProductsControllerBase {
		#region Actions

		public ActionResult Index() {
			var res = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(Context.Products.Include(_ => _.Contents));
			return View(res);
		}

		public ActionResult Delete(int? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = Context.Products.Find(id);
			if (product == null) {
				return HttpNotFound();
			}
			Context.Products.Remove(product);
			Context.SaveChanges();
			return Json(true, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		/*[ValidateAntiForgeryToken]*/
		public JsonResult Edit(EditProductDto dto) {
			if (!ModelState.IsValid) return JsonAffirmation(dto);

			try {
				var res = EditProduct(Mapper.Map<EditProductDto, Product>(dto));
				return dto.Id == -1 ? Json(res) : JsonAffirmation();
			} catch (Exception ex) {
				return JsonAffirmation(null, ex);
			}
		}

		#endregion

		private int EditProduct(Product prod) {
			using (var ta = Context.Database.BeginTransaction()) {
				try {
					var isNewProd = prod.Id == -1;

					if (isNewProd) {
						Context.Entry(prod).State = EntityState.Added;
					}

					UpdateContents(prod);
					Context.SaveChanges();

					ta.Commit();
					return prod.Id;
				} catch (Exception ex) {
					ta.Rollback();
					throw new Exception("Edit product exception", ex);
				}
			}
		}

		private void UpdateContents(Product prod) {
			var newConts = prod.Contents.Where(_ => _.Id == -1).ToList();
			
			var oldConts = Context.Contents.Where(_ => _.Product.Id == prod.Id).ToList();
			var addedConts = newConts.Except(oldConts);
			var deletedConts = oldConts.Except(newConts).ToList();
			var updatedConts = newConts.Except(deletedConts);

			addedConts.ToList().ForEach(_ => Context.Contents.Add(_));

			foreach (var cont in updatedConts) {
				Context.Contents.Attach(cont);
				Context.Entry(cont).State = EntityState.Added;
			}

			deletedConts.ToList().ForEach(_ => Context.Contents.Remove(_));

			Context.Contents.AddRange(newConts);
		}

		private string GetErrorMessage(Exception ex) {
			var res = ex.Message;

			if (ex.InnerException != null)
				res += ex.InnerException.Message;

			return res + ex.StackTrace;
		}
	}
}