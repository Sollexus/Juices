using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JuicesMvc;
using JuicesMvc.Controllers;

namespace JuicesMvc.Tests.Controllers {
	[TestClass]
	public class HomeControllerTest {
		[TestMethod]
		public void Index() {
			// Arrange
			var ser = new JavaScriptSerializer();
			var res = ser.Serialize(new {A = "lol", Bd = 3});
			Debug.Write(res);
		}
	}
}
