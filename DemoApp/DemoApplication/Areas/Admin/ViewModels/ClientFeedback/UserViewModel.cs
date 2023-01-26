namespace DemoApplication.Areas.Admin.ViewModels.ClientFeedback
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public UserViewModel(int id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
