using DemoApplication.Areas.Client.ViewModels.ShopPage;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageTag")]

    public class ShopPageTag :ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageTag(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Tags
                .Select(c => new TagListItemViewModel(c.Id, c.Title))
                .ToListAsync();

            return View(model);
        }
    }
}
