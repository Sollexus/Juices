using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
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
		public void Index() {
			var a = new A {Name = "Lol"};
			
			Mapper.CreateMap<A, B>();
			var b = Mapper.Map<A, B>(a);

			Debug.Write(b.Name);
		}
	}
}
