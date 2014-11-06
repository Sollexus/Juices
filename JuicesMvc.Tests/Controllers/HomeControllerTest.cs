using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using Juices.DAL.Entities.Product;
using JuicesMvc.App_Start;
using JuicesMvc.Dtos.Products;
using JuicesMvc.Models.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JuicesMvc;
using JuicesMvc.Controllers;

namespace JuicesMvc.Tests.Controllers {
	[TestClass]
	public class HomeControllerTest {
		class Item {
			public int Id { get; set; }
		}
		class A {
			public string Name { get; set; }


		}

		class B {
			public string Name { get; set; }
		}

		[TestMethod]
		public void AutoMapperExps() {
			Mapper.Configuration.AllowNullDestinationValues = false;

			Mapper
				.CreateMap<EditProductDto, Product>()
				.Ignore(p => p.Contents)
				.Ignore(p => p.Technologies);

			Mapper.AssertConfigurationIsValid();

			var dto = new EditProductDto {Name = "Something", Description = "Smth"};

			var prod = Mapper.Map<EditProductDto, Product>(dto);

			Assert.AreNotEqual(null, prod.Contents);
		}

		[TestMethod]
		public void Index() {
			var a = new A {Name = "Lol"};
			
			Mapper.CreateMap<A, B>();
			var b = Mapper.Map<A, B>(a);

			Debug.Write(b.Name);
		}
	}
}
