using DemoApplication.Areas.Client.ViewModels.Home;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DemoApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ClientFeedback")]
    public class ClientFeedback : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ClientFeedback(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new IndexViewModel
            {
                ClientFeedbacks = await _dataContext.ClientFeedbacks
                .Select (f => new ClientFeedbackViewModel(f.Id, f.User.FirstName, f.User.LastName, f.User.Roles!.Name, f.Content,
                _fileService.GetFileUrl(f.ImageNameInFileSystem, UploadDirectory.ClientFeedbacks)))
                .ToListAsync()
            };


            return View(model);
        }
    }
}
