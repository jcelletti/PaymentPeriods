using Microsoft.AspNet.Mvc;

namespace JMC.Web.Controllers.Views
{
	[Route("[controller]")]
	public class ViewBaseController : Controller
	{
		[Route("")]
		public virtual IActionResult Index()
		{
			return this.View();
		}
	}
}
