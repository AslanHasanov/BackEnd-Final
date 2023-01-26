using DemoApplication.Database.Models.Common;
using System.Data;

namespace DemoApplication.Database.Models
{
    public class User : BaseEntity<int>, IAuditable
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ClientFeedback> ClientFeedbacks { get; set; }
        public UserActivation? UserActivation { get; set; }

        public int? RoleId { get; set; }
        public Role? Roles { get; set; }


    }
}
