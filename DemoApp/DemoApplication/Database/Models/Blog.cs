using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Blog :BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public List<BlogDisplay>? BlogDisplays { get; set; }

        public List<BlogAndBlogCategory>? BlogAndCategories { get; set; }
        public List<BlogAndBlogTag>? BlogAndTags { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
