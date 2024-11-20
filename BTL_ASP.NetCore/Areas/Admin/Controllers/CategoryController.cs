using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using X.PagedList;

namespace BTL_ASP.NetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ConnectDb _connectDb;
        public CategoryController(ConnectDb connectDb)
        {
            _connectDb = connectDb;

        }
        //Get: category
       
        public async Task< IActionResult> Index(int page = 1)
        {
            int limit = 3;//mổi trang hiển thị 3 bản ghi           
            var cate = await _connectDb.Categories.OrderBy(x => x.CategoryId).ToPagedListAsync(page, limit);
            // nếu không rỗng tham số name trên url

            cate = await _connectDb.Categories.OrderBy(x => x.CategoryId).ToPagedListAsync(page, limit);
            
            return View(cate);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Posst
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _connectDb.Add(category);
                await _connectDb.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        
        //Get edit
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _connectDb.Categories==null)
            {
                return NotFound();
            }
            var category =await _connectDb.Categories.SingleOrDefaultAsync(x=>x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //Posst
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(int? id, Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                    _connectDb.Update(category);
                    await _connectDb.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _connectDb.Categories.SingleOrDefaultAsync(x => x.CategoryId == id);
            _connectDb.Categories.Remove(category);
            await _connectDb.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var category = await _connectDb.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}
