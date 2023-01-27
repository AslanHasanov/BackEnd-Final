using DemoApplication.Areas.Client.ViewModels.Home;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DemoApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "PaymentBenefitsShop")]
    public class PaymentBenefitsShop : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentBenefitsShop(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new IndexViewModel
            {
                PaymentBenefits = await _dataContext.PaymentBenefits.Select(p => new PaymentBenefitsViewModel
                (p.Id, p.Title, p.Content, _fileService.GetFileUrl
                (p.BackgroundImageInFileSystem, UploadDirectory.PaymentBenefits))).ToListAsync()
            };



            return View(model);
        }
    }
}
