using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Areas.Admin.ViewModels.Prize
{
    public class AddViewModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
