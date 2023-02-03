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
                Sliders = await _dataContext.Sliders
                .Select(s => new SliderViewModel(s.Id, s.HeadTitle, s.MainTitle, s.Content, s.Button, s.ButtonRedirectUrl,
                _fileService.GetFileUrl(s.BackgroundImageInFileSystem, UploadDirectory.Slider))).ToListAsync(),

                PaymentBenefits = await _dataContext.PaymentBenefits.Select
                (p => new PaymentBenefitsViewModel
                (p.Id, p.Title, p.Content, _fileService.GetFileUrl(p.BackgroundImageInFileSystem, UploadDirectory.PaymentBenefits))).ToListAsync()
            };

            return View(model);
        }


        [HttpGet("GetProduct/{id}", Name = "Product-GetProduct")]
        public async Task<ActionResult> GetProductAsync(int id)
        {
            var product = await _dataContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (product is null) return NotFound ();
        

            var model = new ModalViewModel(product.Name!, product.Description!, product.Price,
                product.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,

                _dataContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                .Select(pc => new ModalViewModel.ColorViewModeL(pc.Color!.Name, pc.Color.Id)).ToList(),

                _dataContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ModalViewModel.SizeViewModeL(ps.Size!.Title, ps.Size.Id)).ToList() );


            return PartialView("~/Areas/Client/Views/Shared/_ProductModalPartial.cshtml", model);
        }


        [HttpGet("indexsearch", Name = "client-homesearch-index")]
        public async Task<IActionResult> Search(string searchBy, string search)
        {

            return RedirectToRoute("client-shoppage-index", new { searchBy = searchBy, search = search });

        }
    }
}
