using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace JuicesMvc.Helpers {
	public class JsonNetResult : JsonResult {
		public Encoding ContentEncoding { get; set; }
		public string ContentType { get; set; }
		public object Data { get; set; }

		public JsonSerializerSettings SerializerSettings { get; set; }
		public Formatting Formatting { get; set; }

		public JsonNetResult() {
			SerializerSettings = new JsonSerializerSettings();
		}

		public override void ExecuteResult(ControllerContext context) {
			if (context == null)
				throw new ArgumentNullException("context");

			var response = context.HttpContext.Response;

			response.ContentType = !string.IsNullOrEmpty(ContentType)
			  ? ContentType
			  : "application/json";

			if (ContentEncoding != null)
				response.ContentEncoding = ContentEncoding;

		    if (Data == null) return;

		    var writer = new JsonTextWriter(response.Output) { Formatting = Formatting };

		    var serializer = JsonSerializer.Create(SerializerSettings);
		    serializer.Serialize(writer, Data);

		    writer.Flush();
		}
	}
}