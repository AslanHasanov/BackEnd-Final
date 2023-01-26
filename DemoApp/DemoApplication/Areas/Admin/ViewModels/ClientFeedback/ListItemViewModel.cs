namespace DemoApplication.Areas.Admin.ViewModels.ClientFeedback
{
    public class ListItemViewModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ListItemViewModel(int id, string name, string role, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Role = role;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
