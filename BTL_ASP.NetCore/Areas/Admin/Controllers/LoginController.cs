using BTL_ASP.NetCore.Areas.Admin.Models;
using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace BTL_ASP.NetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
   
    public class LoginController : Controller
    {
        private readonly ConnectDb _connectDb;
        public LoginController(ConnectDb connectDb)
        {
            this._connectDb = connectDb;

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(string username, string password)
        {
            //mã hoá pass
            var md5pass = Utilitie.MDH5Hash(password);

            //ktra nguoi dung trong db
            var acc = _connectDb.Accounts.FirstOrDefault(x=>x.UserName == username && x.Password == md5pass);
            if (acc != null && acc.Role=="Admin")
            {
                var identity = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, acc.UserName),
                    new Claim(ClaimTypes.GivenName, acc.FullName),
                    new Claim(ClaimTypes.Role, acc.Role)
                }, "CookieAuthenication");
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(principal);
                return Redirect("~/Admin/Home/Index");
            }
            else
            {
                ViewBag.err = "<div class='alert alert-danger'>Thông tin đăng nhập sai hoặc bạn không có quyền</div>";
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("~/Admin/Home/Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
