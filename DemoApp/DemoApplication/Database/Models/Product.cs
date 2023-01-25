using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Product : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public int? Rate { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductCategory>? ProductCategories { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<ProductColor>? ProductColors { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
        public List<ProductTag>? ProductTags { get; set; }




    }
}
