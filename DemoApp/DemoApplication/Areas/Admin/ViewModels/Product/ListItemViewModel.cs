namespace DemoApplication.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Rate { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<CategoryViewModeL> Categories { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public ListItemViewModel(int id, string name, int? rate, string description, int price, DateTime createdAt, 
            DateTime updatedAt, List<CategoryViewModeL> categories, List<ColorViewModeL> colors, List<SizeViewModeL> sizes, List<TagViewModel> tags)
        {
            Id = id;
            Name = name;
            Rate = rate;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Categories = categories;
            Colors = colors;
            Sizes = sizes;
            Tags = tags;
        }

        public class CategoryViewModeL
        {
          
            public string Title { get; set; }
            public string ParentTitle { get; set; }

            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }
        }
        public class SizeViewModeL
        {
            public string Title { get; set; }
            public SizeViewModeL(string title)
            {
                Title = title;
            }
        }
        public class ColorViewModeL
        {
            public string Name { get; set; }
            public ColorViewModeL(string name)
            {
                Name = name;
            }
        }
        public class TagViewModel
        {
            public string Title { get; set; }
            public TagViewModel(string title)
            {
                Title = title;
            }

     
        }

     
    }
}
