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
			IEnumerable<ProductViewModel> res =
				Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(Context.Products.Include(_ => _.Contents));
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
		public ActionResult Edit(EditProductDto dto) {
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
						Context.Products.Add(prod);
						Context.SaveChanges();
					}

					var newConts = prod.Contents.Where(_ => _.Id == -1);
					var oldConts = Context.Contents.Where(_ => _.Product.Id == prod.Id);

					Context.Contents.AddRange(newConts);
					Context.SaveChanges();

					ta.Commit();
					return prod.Id;
				} catch {
					ta.Rollback();
					throw;
				}
			}
		}

		private string GetErrorMessage(Exception ex) {
			var res = ex.Message;

			if (ex.InnerException != null)
				res += ex.InnerException.Message;

			return res + ex.StackTrace;
		}
	}
}