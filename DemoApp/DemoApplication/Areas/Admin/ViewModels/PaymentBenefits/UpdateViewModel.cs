using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Areas.Admin.ViewModels.PaymentBenefits
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public IFormFile? BackgroundImage { get; set; }
        [Required]
        public string BackgroundImageInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
