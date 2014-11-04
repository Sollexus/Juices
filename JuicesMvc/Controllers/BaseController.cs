using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using JuicesMvc.Helpers;
using Newtonsoft.Json;

namespace JuicesMvc.Controllers {
	public class Affirmation {
		public Affirmation(bool success, object modelErrors = null, object customError = null) {
			Success = success;
			ModelErrors = modelErrors;
			CustomError = customError;
		}

		public bool Success { get; set; }
		public object ModelErrors { get; set; }
		public object CustomError { get; set; }
	}

	public class BaseController : Controller {
		protected override JsonResult Json(object data, string contentType, Encoding contentEncoding) {
			return new JsonNetResult {
				Data = data,
				ContentEncoding = contentEncoding
			};
		}

		protected JsonResult JsonAffirmation(object model, object customError = null) {
			if (model == null)
				return Json(new Affirmation(false, customError: customError));

			return Json(!ModelState.IsValid
				? new Affirmation(false, JsonConvert.SerializeObject(ViewData.ModelState), customError)
				: new Affirmation(true));
		}

		protected JsonResult JsonAffirmation() {
			return Json(new Affirmation(true));
		}

		protected object JsonModelErrors(object model) {
			var modelType = model.GetType();

			return ModelState.Select(kvp => new {
				field = GetJsonProperty(modelType, kvp.Key),
				errors = kvp.Value.Errors.Select(err => err.ErrorMessage).ToList()
			}).ToArray();
		}

		private static string GetJsonProperty(Type modelType, string property) {
			PropertyInfo propertyInfo = modelType.GetProperty(property);
			object[] attrs = propertyInfo.GetCustomAttributes(typeof (JsonPropertyAttribute), false);
			return attrs.Length == 1 ? ((JsonPropertyAttribute) attrs[0]).PropertyName : property;
		}
	}
}