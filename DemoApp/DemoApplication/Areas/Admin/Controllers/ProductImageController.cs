using DemoApplication.Areas.Admin.ViewModels.ProductImage;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/productimage")]
    public class ProductImageController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public ProductImageController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }



        #region List'

        [HttpGet("{productId}/image/list", Name = "admin-productimage-list")]

        public async Task<IActionResult> ListAsync([FromRoute] int productId)
        {
            var product = await _dataContext.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(pi => pi.Id == productId);

            if (product == null) { return NotFound(); }

            var model = new ListItemViewModel { ProductId = product.Id };

            model.Images = product.ProductImages!.Select(p => new ListItemViewModel.ListItem
            {
                Id = p.Id,
                ImageUrl = _fileService.GetFileUrl(p.ImageNameInFileSystem, Contracts.File.UploadDirectory.Products),
                CreatedAt = p.CreatedAt
            }).ToList();


            return View(model);
        }
        #endregion


        #region Add'

        [HttpGet("{productId}/image/add", Name = "admin-productimage-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{productId}/image/add", Name = "admin-productimage-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int productId, AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == productId);


            if (product is null) return NotFound();
            
            

            var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Products);

            var productImage = new ProductImage
            {
                Product = product,
                ImageName = model.Image.FileName,
                ImageNameInFileSystem = imageNameInSystem,
            };

            await _dataContext.ProductImages.AddAsync(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-productimage-list", new { productId = productId });

        }
        #endregion


        #region Delete'

        [HttpPost("{productId}/image/{productImageId}/delete", Name = "admin-productimage-delete")]
        public async Task<IActionResult> Delete([FromRoute] int productId, [FromRoute] int productImageId)
        {

            var productImage = await _dataContext.ProductImages
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.Id == productImageId);

            if (productImage is null) return NotFound();
          

            await _fileService.DeleteAsync(productImage.ImageNameInFileSystem, UploadDirectory.Products);

            _dataContext.ProductImages.Remove(productImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-productimage-list", new { productId = productId });

        }
        #endregion
    }
}
