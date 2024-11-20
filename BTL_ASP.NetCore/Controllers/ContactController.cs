using Microsoft.AspNetCore.Mvc;

namespace BTL_ASP.NetCore.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet("/Contact")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
