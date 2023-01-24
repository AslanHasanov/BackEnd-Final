using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Areas.Admin.ViewModels.PaymentBenefits
{
    public class AddViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public IFormFile? BackgroundImage { get; set; }
        public string BackgroundImageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
