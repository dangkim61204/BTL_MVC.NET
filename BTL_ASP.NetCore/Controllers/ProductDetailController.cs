using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_ASP.NetCore.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ConnectDb _context;
        public ProductDetailController(ConnectDb context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(int id)
        {
            
            var p = _context.Products.Include("Category") .Where(x=>x.ProductId==id).FirstOrDefault();
           
            if (p == null)
            {
                return NotFound();
            }

            ViewBag.Category = p.Category;
            return View(p);
        }
    }
}
