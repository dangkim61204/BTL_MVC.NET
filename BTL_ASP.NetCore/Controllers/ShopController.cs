using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_ASP.NetCore.Controllers
{
    public class ShopController : Controller
    {       
        private readonly ConnectDb _context;
        public ShopController(ConnectDb context)
        {
            _context = context;
        }

        [HttpGet("/shop")]
        public IActionResult Index(string name, string sortOrder, int page = 1, int id = 0, float fromPrice=0, float toPrice=0)
        {
            int limit = 4; // Lấy ra 3 bản ghi 1 trang
            // Cấu hình tham số ViewBag để phục vụ cho sắp xếp
            ViewBag.PriceSortParam = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "price_asc"; // Chuyển đổi để sắp xếp giá
            ViewBag.NameSortParam = sortOrder == "name_desc" ? "name_asc" : "name_desc"; // Chuyển đổi để sắp xếp tên

            // Lấy danh sách sản phẩm và bao gồm thông tin danh mục
            var products = _context.Products.Include(p => p.Category).AsQueryable();
            var categories = _context.Categories.AsQueryable();
            
            // Lọc theo tên sản phẩm nếu có tham số 'name'
            if (!string.IsNullOrEmpty(name))
            {      
                
                    products = products.Where(x => x.ProductName.Contains(name));   
            }
            
            // Lọc theo danh mục nếu có tham số 'id'
            if (id != 0)
            {
                products = products.Where(x => x.CategoryId == id);
            }

            // Sắp xếp theo giá hoặc tên tùy vào giá trị của 'sortOrder'
            switch (sortOrder)
            {
                case "price_desc": // Giá giảm dần
                    products = products.OrderByDescending(x => x.Price);
                    break;
                case "price_asc": // Giá tăng dần
                    products = products.OrderBy(x => x.Price);
                    break;
                case "name_desc": // Tên Z-A
                    products = products.OrderByDescending(x => x.ProductName);
                    break;
                case "name_asc": // Tên A-Z
                    products = products.OrderBy(x => x.ProductName);
                    break;
                default: // Mặc định sắp xếp theo tên A-Z
                    products = products.OrderBy(x => x.ProductName);
                    break;
            }
            ViewBag.Sort = sortOrder; 

            // Lấy thông tin danh mục để hiển thị nếu cần
            if (id != 0)
            {
                var category = _context.Categories.Find(id);
                if (category != null)
                {
                    ViewBag.CategoryName = category.CategoryName;
                }
            }
            else
            {
                ViewBag.CategoryName = "Tất cả sản phẩm"; 
            }

            // Phân trang với PagedList
            var shop = products.ToPagedList(page, limit);

            // Truyền dữ liệu vào View
            ViewBag.Products = shop; // Đây là danh sách đã phân trang và lọc
            ViewBag.Categories = categories;
            return View(shop);
        }

     

    }
}
