using BTL_ASP.NetCore.Areas.Admin.Models.DataModels;

namespace BTL_ASP.NetCore.Areas.Admin.Models.ViewModels
{
    public class CartItemView
    {
        public List<CartItem> Carts { get; set; }
        public float GrandTotal { get; set; }
    }
}
