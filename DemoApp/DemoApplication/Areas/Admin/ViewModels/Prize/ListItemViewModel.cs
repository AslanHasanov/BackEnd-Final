namespace DemoApplication.Areas.Admin.ViewModels.Prize
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public ListItemViewModel(int id, string? imageUrl, DateTime createdAt)
        {
            Id = id;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
        }

    }
}
