
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace BTL_ASP.NetCore.Areas.Admin.Models.DataModels
{
    [Table("categories")]
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Mã Loại")]
        public int CategoryId { get; set; }

        [DisplayName(" Tên danh mục sản phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên loại sách"), StringLength(100)]
        public string CategoryName { get; set; }

        [DisplayName("Trạng Thái")]
        private bool Active { get; set; }

        // lien ket mot-nhieu với product
        public virtual ICollection<Product> Products { get; set; }
    }
}
