using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_ASP.NetCore.Areas.Admin.Models.DataModels
{
    [Table("Orders")]
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Mã Đặt Hàng")]
        public string OrderId { get; set; }

        [DisplayName("Tên Khách Hàng")]
        public string UserName { get; set; }

        [DisplayName("Trạng Thái")]
        public bool Active { get; set; }

        [DisplayName("Ngày Đặt Hàng")]
        public DateTime OrderDate { get; set; }

  

    }
}
