namespace BTL_ASP.NetCore.Areas.Admin.Models.DataModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float TotalPrice => Quantity * Price;

        public CartItem() { }

        public CartItem(Product product)
        {
            Id = product.ProductId;
            Name = product.ProductName;
            Price = product.Price;
            Quantity = 1;
            Image = product.Image;


        }
    }
}
