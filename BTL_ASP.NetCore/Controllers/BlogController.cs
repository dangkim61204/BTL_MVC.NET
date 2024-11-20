using Microsoft.AspNetCore.Mvc;

namespace BTL_ASP.NetCore.Controllers
{
    public class BlogController : Controller
    {
		[HttpGet("/Blog")]
		public IActionResult Index()
        {
            return View();
        }
    }
}
