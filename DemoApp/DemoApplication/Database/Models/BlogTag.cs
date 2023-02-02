using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class BlogTag : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public List<BlogAndBlogTag> Tags { get; set; }

        public DateTime CreatedAt { get ; set ; }
        public DateTime UpdatedAt { get ; set ; }
    }
}
