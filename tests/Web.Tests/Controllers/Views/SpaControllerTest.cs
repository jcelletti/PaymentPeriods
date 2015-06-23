using JMC.Web.Controllers.Views;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace JMC.Web.Tests.Controllers.Views
{
	public class SpaControllerTest
	{
		[Fact]
		public void IsValid()
		{
			var sut = new SpaController();

			IActionResult result = sut.Index();

			Assert.IsType<ViewResult>(result);

			//var view = (ViewResult)result;
		}
	}
}
