using DemoApplication.Areas.Admin.ViewModels.Blog;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    [Authorize(Roles = "admin")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public readonly ILogger<BlogController> _logger;

        public BlogController(DataContext dataContext, IFileService fileService, ILogger<BlogController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        #region List'

        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> List()
        {

            var model = await _dataContext.Blogs.OrderByDescending(p => p.CreatedAt)
                .Select(p => new ListItemViewModel(p.Id, p.Title, p.Content, p.CreatedAt,
                p.BlogAndTags.Select(ps => ps.Tag)
                .Select(s => new ListItemViewModel.TagViewModel(s.Title)).ToList(),
                p.BlogAndCategories.Select(pc => pc.Category)
                .Select(c => new ListItemViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList()
              
                )).ToListAsync();


            return View(model);
        }

        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.BlogCategories
                    .Select(c => new BlogCategoryViewModel(c.Id, c.Title))
                    .ToListAsync(),
                Tags = await _dataContext.BlogTags
                .Select(t => new BlogTagViewModel(t.Id, t.Title))
                .ToListAsync()
            };

            return View(model);
        }
        #endregion
    }
}
