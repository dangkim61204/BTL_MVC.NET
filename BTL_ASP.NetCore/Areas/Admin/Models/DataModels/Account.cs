using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_ASP.NetCore.Areas.Admin.Models.DataModels
{
    [Table("Accounts")]
    public class Account
    {
        //public Account()
        //{
        //    Orders = new List<Order>();
        //}


        [Key]
        [DisplayName("Mã TK"), StringLength(10), Column(TypeName = "varchar")]
        public string AccountId { get; set; }

        [DisplayName("Tên Đăng Nhập")]
        
        public string UserName { get; set; }

        [DisplayName("Mật Khẩu")]
        public string Password { get; set; }

        [DisplayName("Tên Hiển Thị")]
        public string FullName { get; set; }

        [DisplayName("Giới Tính")]
        public bool Gender { get; set; }
   
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Điện Thoại")]
        public string Phone { get; set; }

        [DisplayName("Ngày Sinh")]
        public DateTime? Birthday { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        public string Role { get; set; }

        // lien ket mot-nhieu với Order
        //public virtual ICollection<Order> Orders { get; set; }
    }
}
