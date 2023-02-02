using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class BlogAndBlogCategory : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory Category { get; set; }
    }
}
