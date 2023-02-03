using DemoApplication.Database.Models.Enum;

namespace DemoApplication.Areas.Client.ViewModels.Account
{
    public class OrderViewModel
    {
      

        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }

        public OrderViewModel(string id, DateTime createdAt, OrderStatus status, decimal total)
        {
            Id = id;
            CreatedAt = createdAt;
            Status = status;
            Total = total;
        }
    }
}
