using DemoApplication.Areas.Client.ViewModels.ShopPage;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DemoApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageCategory")]

    public class ShopPageCategory : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageCategory(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Categories
                .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                .ToListAsync();

            return View(model);
        }
    }
}
