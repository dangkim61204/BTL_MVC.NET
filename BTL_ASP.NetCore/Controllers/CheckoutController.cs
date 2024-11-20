using BTL_ASP.NetCore.Areas.Admin.Models;
using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTL_ASP.NetCore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ConnectDb _context;
        public CheckoutController(ConnectDb context)
        {
            _context = context;
        }

        [HttpGet("/Checkout")]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Redirect("/Login");
            }
            else
            {
                var orderId = Guid.NewGuid().ToString();
                var orderItem = new Order();
                orderItem.OrderId = orderId;
                orderItem.UserName = userEmail; 
                orderItem.Active = true;
                orderItem.OrderDate = DateTime.Now;
                _context.Add(orderItem);
                _context.SaveChanges();
                List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
                foreach(var item in cartItems)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderId = orderId;
                    orderDetail.UserName = userEmail;
                    orderDetail.ProductId= item.Id;
                    orderDetail.Price = item.Price;
                    orderDetail.Quantity=item.Quantity;
                    _context.Add(orderDetail);
                    _context.SaveChanges();
                }
                TempData["Success"] = "đơn hàng đã được tạo";

                return Redirect("~/Home/Index");

            }
            return View();
            
        }


    }
}
