using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DemoApplication.Areas.Admin.ViewModels.ClientFeedback
{
    public class AddViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }
        public List<UserViewModel>? Users { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
