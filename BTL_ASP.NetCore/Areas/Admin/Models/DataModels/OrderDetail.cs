using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_ASP.NetCore.Areas.Admin.Models.DataModels
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [DisplayName("Mã đơn hàng")]
        public string OrderId { get; set; }

        [DisplayName("Mã sản phẩm")]
        public int ProductId { get; set; }

        [DisplayName("Số lượng")]
        public float Quantity { get; set; }

        [DisplayName("Giá sản phẩm")]
        public float Price { get; set; }

        [DisplayName("Tên Khách Hàng")]
        public string UserName { get; set; }
    
    }
}
