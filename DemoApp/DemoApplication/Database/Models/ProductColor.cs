using DemoApplication.Database.Models.Common;
using System.Drawing;

namespace DemoApplication.Database.Models
{
    public class ProductColor:BaseEntity<int>
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public int ColorId { get; set; }
        public Color? Color { get; set; }
    }
}
