using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Transactions;
using AutoMapper;
using Juices.DAL;
using Juices.DAL.Entities.Product;
using JuicesMvc.App_Start;
using JuicesMvc.Areas.Admin.Controllers;
using JuicesMvc.Controllers;
using JuicesMvc.Dtos.Products;
using JuicesMvc.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using IsolationLevel = System.Data.IsolationLevel;


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
		public void CodeFirstExperiments() {
			using (var context = new JuicyContext()) {
				using (var ta = context.Database.BeginTransaction(IsolationLevel.ReadUncommitted)) {
					var prod = new Product {
						Id = -1,
						Name = "Name",
						Description = "Descr",
						Contents = new List<Content> {
							new Content {Id = -1, ChemicalId = 34}
						}
					};

					try {
						context.Products.Add(prod);
						context.SaveChanges();

						DbToConsole(context);
					} catch (Exception ex) {
						Console.WriteLine(ex.ToJson(_jsonSettings, Formatting.Indented));
						throw;
					}
				}
			}
		}

		[TestMethod]
		public void EditProduct() {
			using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted })) {
				using (var context = new JuicyContext()) {
					var prodGuid = Guid.NewGuid().ToString();

					var dtoWithNew = new EditProductDto {
						Id = -1,
						Name = "dtoWithNew",
						Description = prodGuid,
						Contents = new List<ContentDto> {
							new ContentDto {Id = -1, ChemicalId = context.Chemicals.First().Id}
						}
					};

					var addNewRes = new ProductsController().Edit(dtoWithNew);
					DbToConsole(context);

					Assert.IsInstanceOfType(addNewRes.Data, typeof (int));
					Assert.AreNotSame((int) addNewRes.Data, -1);

					var prod = context.Products.First(_ => _.Description == prodGuid);
					Assert.AreEqual(1, prod.Contents.Count);

					var dtoToTestDelete = new EditProductDto {
						Id = prod.Id,
						Name = "dtoUpdated"
					};

					var delContsRes = new ProductsController().Edit(dtoToTestDelete);
					prod = context.Products.Find(prod.Id);
					Assert.AreEqual(0, prod.Contents.Count);

					Console.WriteLine("\n/********************  AFTER POSSIBLE DELETION  *******************/\n");
					DbToConsole(context);

					Assert.IsInstanceOfType(delContsRes.Data, typeof (Affirmation));

					var affirmation = delContsRes.Data as Affirmation;
					Debug.WriteLine(JsonConvert.SerializeObject(affirmation, Formatting.Indented, _jsonSettings));
					if (!affirmation.Success) throw affirmation.CustomError;
				}
			}
		}

		private void DbToConsole(JuicyContext context) {
			Console.WriteLine("\n/********************  PRODUCTS  *******************/\n");

			foreach (var product in context.Products.ToList()) {
				Console.WriteLine(product.ToJson(_jsonSettings, Formatting.Indented));
			}

			Console.WriteLine("\n/********************  CONTENTS  *******************/\n");
			
			ContsToConsole(context);
		}

		private void ContsToConsole(JuicyContext context) {
			foreach (var content in context.Contents.ToList()) {
				Console.WriteLine(content.ToJson(_jsonSettings, Formatting.Indented));
			}
		}

		[TestCleanup]
		public void TestCleanup() {

		}
	}
}

