using BTL_ASP.NetCore.Areas.Admin.Models;
using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace BTL_ASP.NetCore.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ConnectDb _context;
        public RegisterController(ConnectDb context)
        {
            _context = context;
        }
        [HttpGet("/Register")]
        public IActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Account acc)
        {
           
            Account acount = new Account
                {                 
                
                    AccountId = GenerateAccountId(10),
                    Email = acc.Email,
                    Password = Utilitie.MDH5Hash(acc.Password),
                    UserName = acc.UserName,
                    FullName = acc.FullName,
                    Address = acc.Address,
                    Phone = acc.Phone,
                    Role = "Admin",
                    Gender = acc.Gender
                };
            // Lưu vào database
                await _context.Accounts.AddAsync(acount);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
           
           
        }

        public string GenerateAccountId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Ví dụ tạo accountId dài 10 ký tự
        

    }
}
