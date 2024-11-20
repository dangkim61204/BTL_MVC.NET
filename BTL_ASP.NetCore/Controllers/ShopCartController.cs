using Microsoft.AspNetCore.Mvc;
using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using BTL_ASP.NetCore.Areas.Admin.Models;
using BTL_ASP.NetCore.Areas.Admin.Models.ViewModels;
namespace BTL_ASP.NetCore.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ConnectDb _context;
        public ShopCartController(ConnectDb context)
        {
            _context = context;
        }
        [HttpGet("/Shop-Cart")]
        public IActionResult Index()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItemView cartView = new()
            {
                Carts = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price),

            };

            return View(cartView);
        }


        //async baats doong bo
        public async Task<IActionResult> AddCart(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product != null) {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                CartItem cartItem = cart.Where(x => x.Id == id).FirstOrDefault();

                if (cartItem == null)
                {

                    cart.Add(new CartItem
                    {
                        Id = product.ProductId,
                        Name = product.ProductName,
                        Image = product.Image,
                        Price = product.Price,
                        Quantity = 1

                    });
                }
                else
                {
                    cartItem.Quantity += 1;

                }
                HttpContext.Session.SetJson("Cart", cart);
                return Redirect(Request.Headers["Referer"].ToString());
            }

            return NotFound();

        }

        //tang giam so luong sp
        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem item = cart.Where(x=>x.Id == id).FirstOrDefault();

            if (item.Quantity > 1 ) 
            {
                --item.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.Id == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Increase(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem item = cart.Where(x => x.Id == id).FirstOrDefault();

            if (item.Quantity >= 1)
            {
                ++item.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.Id == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            cart.RemoveAll(x=>x.Id == id);

            if (cart.Count == 0) {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear(int id)
        {
             HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
    }
}
