
using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class ProductCategory :BaseEntity<int> 
    {
        public Product? Product { get; set; }
        public int ProductId { get; set; }


        public Category? Category { get; set; }
        public int CatagoryId { get; set; }



    }
}
