using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Areas.Admin.ViewModels.Slider
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string HeadTitle { get; set; }
        [Required]
        public string MainTitle { get; set; }
        [Required]
        public string Content { get; set; }
        public IFormFile BackgroundImage { get; set; }
        public string? BackgroundImageUrl { get; set; }
        [Required]
        public string Button { get; set; }
        [Required]
        public string ButtonRedirectUrl { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

