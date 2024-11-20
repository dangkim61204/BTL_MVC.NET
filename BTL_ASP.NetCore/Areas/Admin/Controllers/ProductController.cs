using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace BTL_ASP.NetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ConnectDb _context;
        public ProductController(ConnectDb context)
        {
            _context = context;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5;//mổi trang hiển thị 3 bản ghi
            var connectDb = _context.Products.Include(p => p.Category);
            var products = await _context.Products.OrderBy(x=>x.ProductId).ToPagedListAsync(page, limit);
            // nếu không rỗng tham số name trên url
            if (!string.IsNullOrEmpty(name))
            {
                products = await _context.Products.Where(x => x.ProductName.Contains(name)).OrderBy(x=>x.ProductId).ToPagedListAsync(page,limit);
            }
         
            return View(products);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Code,ProductName,Image,Images,Price,SalePrice,Desciption,CategoryId")] Product product, IFormFile filePicture)
        {
            if (filePicture != null && filePicture.Length > 0)
            {
                string pathfile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filePicture.FileName);
                using (var stream = System.IO.File.Create(pathfile))
                {
                    await filePicture.CopyToAsync(stream);
                }
                product.Image = "/images/" + filePicture.FileName;
            }
            else
            {
                ViewBag.msg = "Ảnh không được trống";
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
            Debug.WriteLine(product.Image + "abcccc");
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile? filePicture, Product product)
        {
       
            ////xu li upload
            if (filePicture != null && filePicture.Length > 0)
            {
                string pathfile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filePicture.FileName);
                using (var stream = System.IO.File.Create(pathfile))
                {
                    await filePicture.CopyToAsync(stream);
                }
                product.Image = "/images/" + filePicture.FileName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Debug.WriteLine("MMMMMM");
                        throw;
                    }
                }
                Debug.WriteLine("MrrrrMMMMM");
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.SingleOrDefaultAsync(x => x.ProductId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        //search
        //public IActionResult Index(string name)
        //{
        //   var products = _context.Products.ToList();
        //    if(!string.IsNullOrEmpty(name) )
        //    {
        //        products = _context.Products.Where(x=>x.ProductName.Contains(name)).ToList();
        //    }
        //    return View(products);
        //}
    }
}
