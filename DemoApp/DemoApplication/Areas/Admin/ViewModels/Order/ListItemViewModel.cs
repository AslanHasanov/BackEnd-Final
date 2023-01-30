using DemoApplication.Database.Models.Enum;

namespace DemoApplication.Areas.Admin.ViewModels.Order
{
    public class ListItemViewModel
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }

        public ListItemViewModel(string id, DateTime createdAt, OrderStatus status, decimal total)
        {
            Id = id;
            CreatedAt = createdAt;
            Status = status;
            Total = total;
        }

    }
}
