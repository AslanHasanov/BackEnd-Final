using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using DemoApplication.Areas.Client.ViewModels.Prize;

using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Prize")]
    public class Prize : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Prize(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reward = await _dataContext.Prizes
                .Select(r =>
                new ListItemViewModel
                (r.Id, _fileService.GetFileUrl(r.ImageNameInFileSystem, Contracts.File.UploadDirectory.Prize)))
                .ToListAsync();

            return View(reward);
        }
    }
}
