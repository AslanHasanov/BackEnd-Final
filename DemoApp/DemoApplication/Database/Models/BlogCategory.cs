using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class BlogCategory : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public BlogCategory? Parent { get; set; }
        public List<BlogCategory> Categories { get; set; }
        public List<BlogAndBlogCategory> BlogAndCategories { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
