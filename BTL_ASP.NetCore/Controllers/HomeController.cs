using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using BTL_ASP.NetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using X.PagedList;

namespace BTL_ASP.NetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConnectDb _context;
        public HomeController(ConnectDb context)
        {
            _context = context;
        }

        public IActionResult Index(string name, int page = 1, int id = 0)
        {
            int limit = 4; // Lấy ra 3 bản ghi 1 trang

            // Lấy danh sách sản phẩm và bao gồm thông tin danh mục
            var products = _context.Products.Include(p => p.Category).AsQueryable();
            var categories = _context.Categories.AsQueryable();

            // Lọc theo tên sản phẩm nếu có tham số 'name'
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(x => x.ProductName.Contains(name));
            }
            // Phân trang với PagedList
            var p = products.ToPagedList(page, limit);
     
            return View(p);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
