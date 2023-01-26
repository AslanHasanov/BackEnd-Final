using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class ClientFeedback :BaseEntity<int>, IAuditable
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
