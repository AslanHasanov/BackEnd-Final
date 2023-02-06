﻿namespace DemoApplication.Areas.Client.ViewModels.Home
{
    public class BlogViewModel
    {
       

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BlogDisplays { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<TagViewModel> Tags { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }

        public BlogViewModel(int id, string title, string content, string blogDisplays, bool isImage, bool isVideo,
            DateTime createdAt, List<TagViewModel> tags, List<CategoryViewModeL> categories)
        {
            Id = id;
            Title = title;
            Content = content;
            BlogDisplays = blogDisplays;
            IsImage = isImage;
            IsVideo = isVideo;
            CreatedAt = createdAt;
            Tags = tags;
            Categories = categories;
        }

        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }
    }
}
