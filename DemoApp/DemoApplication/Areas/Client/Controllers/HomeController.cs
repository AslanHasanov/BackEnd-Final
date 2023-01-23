using DemoApplication.Areas.Client.ViewModels.Home;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dataContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("home-index")]
        public async Task <IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Sliders = await _dataContext.Sliders.Select(s => new SliderViewModel(s.Id, s.HeadTitle, s.MainTitle, s.Content, s.Button, s.ButtonRedirectUrl,
                _fileService.GetFileUrl(s.BackgroundImageInFileSystem, UploadDirectory.Slider))).ToListAsync(),

                PaymentBenefits = await _dataContext.PaymentBenefits.Select
                (p => new PaymentBenefitsViewModel
                (p.Id, p.Title, p.Content, _fileService.GetFileUrl(p.BackgroundImageInFileSystem, UploadDirectory.PaymentBenefits))).ToListAsync()
            };

            return View(model);
        }
    }
}
