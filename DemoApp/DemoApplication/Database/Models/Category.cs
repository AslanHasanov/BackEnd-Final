using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Category : BaseEntity<int>,IAuditable
    {
        public string? Title { get; set; }
        public int? ParentId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get ; set; }


        public Category? Parent { get; set; }
        public List<Category>? Categories { get; set; }

        public List<ProductCategory>? ProductCategories { get; set; }

    }
}
