using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Slider :BaseEntity<int>, IAuditable
    {
        public string HeadTitle { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundImageInFileSystem { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
