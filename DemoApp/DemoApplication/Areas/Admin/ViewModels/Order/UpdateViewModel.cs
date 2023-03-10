using DemoApplication.Database.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Areas.Admin.ViewModels.Order
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
