namespace DemoApplication.Areas.Client.ViewModels.Home
{
    public class SliderViewModel
    {
       

        public int Id { get; set; }
        public string HeadTitle { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public string BackGroundImageUrl { get; set; }
        public int Order { get; set; }

        public SliderViewModel(){ }

        public SliderViewModel(int id, string headTitle, string mainTitle, string content, string button, string buttonRedirectUrl, string backGroundImageUrl)
        {
            Id = id;
            HeadTitle = headTitle;
            MainTitle = mainTitle;
            Content = content;
            Button = button;
            ButtonRedirectUrl = buttonRedirectUrl;
            BackGroundImageUrl = backGroundImageUrl;
        }
       
    }
}
