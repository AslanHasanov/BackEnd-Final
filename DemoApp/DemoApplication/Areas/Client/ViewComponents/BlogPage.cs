﻿using DemoApplication.Areas.Client.ViewModels.Home;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "BlogPage")]

    public class BlogPage :ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogPage(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
               await _dataContext.Blogs
               .Include(b => b.BlogAndTags).Include(b => b.BlogAndCategories).Include(b => b.BlogDisplays)
                .Select(b => new BlogViewModel(b.Id, b.Title, b.Content,
                b.BlogDisplays!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogDisplays!.Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.Blog)
                : String.Empty,

                b.BlogDisplays!.FirstOrDefault()!.IsImage != null! ? b.BlogDisplays!.FirstOrDefault()!.IsImage : default,
                b.BlogDisplays!.FirstOrDefault()!.IsVidio != null! ? b.BlogDisplays!.FirstOrDefault()!.IsVidio : default,
                b.CreatedAt,
                b.BlogAndTags!.Select(b => b.Tag).Select(b => new BlogViewModel.TagViewModel(b.Title)).ToList(),
                b.BlogAndCategories!.Select(b => b.Category)
                .Select(b => new BlogViewModel.CategoryViewModeL(b.Title, b.Parent!.Title)).ToList()
                )).ToListAsync();

            return View(model);
        }
    }
}
