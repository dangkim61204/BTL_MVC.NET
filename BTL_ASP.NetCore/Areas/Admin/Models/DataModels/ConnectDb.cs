using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BTL_ASP.NetCore.Areas.Admin.Models.DataModels
{
    public class ConnectDb : DbContext
    {
        public ConnectDb() { }
        public ConnectDb(DbContextOptions<ConnectDb> options)
            : base(options)
        {

        }
        //khai báo các thuộc tính map với bảng
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

    }
}
