using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Product : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public int? Rate { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductCategory>? ProductCategories { get; set; }




    }
}
