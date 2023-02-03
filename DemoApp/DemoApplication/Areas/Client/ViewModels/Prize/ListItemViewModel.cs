namespace DemoApplication.Areas.Client.ViewModels.Prize

{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? prizeImageUrl)
        {
            Id = id;
            PrizeImageUrl = prizeImageUrl;
        }

        public int Id { get; set; }
        public string? PrizeImageUrl { get; set; }
    }
}
