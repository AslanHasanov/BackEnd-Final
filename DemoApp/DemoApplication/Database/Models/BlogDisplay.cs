using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class BlogDisplay : BaseEntity<int>, IAuditable
    {
        public string? FileName { get; set; }
        public string? FileNameInSystem { get; set; }
        public bool IsImage { get; set; }
        public bool IsVidio { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
