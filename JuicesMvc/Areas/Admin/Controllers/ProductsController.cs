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

			EditProduct(dto);

			/*if (product.Id == -1)
				return Json(Create(product));*/

			//Context.Entry(product).State = EntityState.Modified;
			//Context.SaveChanges();
			return JsonAffirmation();
		}

		#endregion

		private int Create(Product product) {
			Context.Products.Add(product);
			Context.SaveChanges();
			return product.Id;
		}

		private void EditProduct(EditProductDto dto) {
			if (dto.Id != -1)
			{
				IQueryable<Content> conts = Context.Contents.Where(_ => _.Product.Id == dto.Id);
				foreach (var content in conts)
				{

				}
				conts.Where(_ => dto.Contents.All(c => c.Id != _.Id));
			}

		}
	}
}