using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Transactions;
using JuicesMvc.App_Start;
using JuicesMvc.Areas.Admin.Controllers;
using JuicesMvc.Controllers;
using JuicesMvc.Dtos.Products;
using JuicesMvc.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace JuicesMvc.Tests.Controllers {
	[TestClass]
	public class ProductControllerTest {
		private JsonSerializerSettings _jsonSettings;

		[TestInitialize]
		public void TestInitialize() {
			MapperConfig.Configure();

		 	var dataDir = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "JuicesMvc\\App_Data");

			AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);

			_jsonSettings = new JsonSerializerSettings {
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				//PreserveReferencesHandling = PreserveReferencesHandling.Objects,
				Converters = new JsonConverter[] { new DbEntryJsonConverter() }
			};
		}

		[TestMethod]
		public void EditProduct() {
			using (var scope = new TransactionScope()) {
				using (var ctrl = new ProductsController()) {
					var dto = new EditProductDto {
						Id = -1,
						Name = "Name",
						Description = "Descr",
						Contents = new List<ContentDto> {
						new ContentDto {Id = -1, ChemicalId = 34}
					}
					};

					var res = ctrl.Edit(dto);
					Assert.IsInstanceOfType(res.Data, typeof(Affirmation));
					var affirmation = res.Data as Affirmation;
					Debug.WriteLine(JsonConvert.SerializeObject(affirmation, Formatting.Indented, _jsonSettings));
					Assert.IsTrue(affirmation.Success);
				}	
			}
		}

		[TestCleanup]
		public void TestCleanup() {
			
		}
	}
}
