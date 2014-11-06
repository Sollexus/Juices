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
using Microsoft.Owin.Logging;

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
			
			var res = EditProduct(dto);

			return res == -1 ? JsonAffirmation(false) : Json(res);
		}

		#endregion
	
		private int EditProduct(EditProductDto dto) {
			var prod = Mapper.Map<EditProductDto, Product>(dto);

			using (var ta = Context.Database.BeginTransaction()) {
				try {
					if (dto.Id == -1) {
						Context.Products.Add(prod);
						Context.SaveChanges();
					}
					
					var newConts = Context.Contents
						.Where(_ => _.Product.Id == dto.Id)
						.Where(_ => dto.Contents.All(c => c.Id != _.Id))
						.Select((_, i) => new Content {ChemicalId = _.ChemicalId, ProductId = prod.Id, Order = i});

					Context.Contents.AddRange(newConts);
					
					ta.Commit();
					return prod.Id;
				} catch (Exception ex) {
					ModelState.AddModelError("CustomError", ex);
					ta.Rollback();
				}
			}

			return -1;
		}
	}
}