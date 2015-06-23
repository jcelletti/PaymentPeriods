using Microsoft.AspNet.Mvc;

namespace JMC.Web.Controllers.Views
{
	[Route("[controller]")]
	[Route("")]
	public class SpaController : Controller
	{
		[Route("Index")]
		[Route("")]
		public IActionResult Index()
		{
			return this.View();
		}
	}
}
