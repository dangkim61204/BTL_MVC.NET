using Microsoft.AspNetCore.Mvc;

namespace BTL_ASP.NetCore.Controllers
{
    public class AboutController : Controller
    {
        [HttpGet("/About-us")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
