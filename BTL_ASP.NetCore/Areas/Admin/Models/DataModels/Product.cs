using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Xml.Linq;

namespace BTL_ASP.NetCore.Areas.Admin.Models.DataModels
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ProductId { get; set; }

        [DisplayName("Mã Sản Phẩm")]
        [Required(ErrorMessage = "Nhập mã sản phẩm"), StringLength(10)]
        public string Code { get; set; }

        [DisplayName("Tên Sản Phẩm")]
        [Required(ErrorMessage = "Hãy nhập tên loại sách"), StringLength(100)]
        public string ProductName { get; set; }

        [DisplayName("Ảnh Sản Phẩm")]

        public string? Image { get; set; } 
        public string? Images { get; set; }

        [DisplayName("Giá Sản Phẩm")]
        [Required(ErrorMessage = "Hãy nhập giá sản phẩm")]
        public float Price { get; set; }

        public float? SalePrice { get; set; }

        [DisplayName("Mô Tả Sản Phẩm")]
        public string? Desciption { get; set; }

        [DisplayName("Danh mục sản phẩm")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        //lien ket nhieu-mot vơi category
        public virtual Category? Category { get; set; }

    }
}
