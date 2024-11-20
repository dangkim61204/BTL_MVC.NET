using Microsoft.AspNetCore.Mvc;

namespace BTL_ASP.NetCore.Controllers
{
    public class PrivacyController : Controller
    {
        [HttpGet("/Privacy")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
