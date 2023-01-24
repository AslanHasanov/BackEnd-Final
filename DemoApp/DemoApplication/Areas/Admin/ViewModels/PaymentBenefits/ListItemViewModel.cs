namespace DemoApplication.Areas.Admin.ViewModels.PaymentBenefits
{
    public class ListItemViewModel
    {
    

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BackgroundImageInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ListItemViewModel(int id, string title, string content, string backgroundImageInFileSystem, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Title = title;
            Content = content;
            BackgroundImageInFileSystem = backgroundImageInFileSystem;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
