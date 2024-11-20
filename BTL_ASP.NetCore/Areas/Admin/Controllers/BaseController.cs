using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static System.Net.Mime.MediaTypeNames;

namespace BTL_ASP.NetCore.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            String? user = User.Identity?.Name;
            String? role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (role == "Admin")
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Login" },
                    { "action", "AccessDenied" } });
            }
        }
    }
}
