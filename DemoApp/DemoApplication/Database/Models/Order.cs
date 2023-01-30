using DemoApplication.Database.Models.Common;
using DemoApplication.Database.Models.Enum;

namespace DemoApplication.Database.Models
{
    public class Order : BaseEntity<string>, IAuditable
    {
        public OrderStatus Status { get; set; }
        public decimal SumTotalPrice { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
