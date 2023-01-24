namespace DemoApplication.Areas.Admin.ViewModels.Slider
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string HeadTitle { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string BackgroundImageUrl { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public ListItemViewModel(int id, string headTitle, string mainTitle, string content,
            string backgroundImageUrl,string button, string buttonRedirectUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            HeadTitle = headTitle;
            MainTitle = mainTitle;
            Content = content;
            BackgroundImageUrl = backgroundImageUrl;
            Button = button;
            ButtonRedirectUrl = buttonRedirectUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
