using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class PaymentBenefit : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackgroundImage { get; set; }
        public string BackgroundImageInFileSystem { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
