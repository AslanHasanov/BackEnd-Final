using DemoApplication.Areas.Client.ViewModels.Home;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Product")]
    public class Product:ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Product(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string slide)
        {
         
            var model = new IndexViewModel
            {
                Products = await _dataContext.Products
                .Take(7).Select(p => new ProductViewModel(p.Id, p.Name!, p.Description!, p.Price,
                p.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,
                p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(p.ProductImages!.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                : String.Empty,
                p.ProductCategories!.Select(pc => pc.Category)
                .Select(c => new ProductViewModel.CategoryViewModeL(c.Title!, c.Parent!.Title!)).ToList(),
                p.ProductColors!.Select(pc => pc.Color)
                .Select(c => new ProductViewModel.ColorViewModeL(c!.Name)).ToList(),
                p.ProductSizes!.Select(ps => ps.Size)
                .Select(s => new ProductViewModel.SizeViewModeL(s!.Title)).ToList(),
                p.ProductTags!.Select(ps => ps.Tag).
                Select(s => new ProductViewModel.TagViewModel(s!.Title)).ToList()))
                 .ToListAsync(),
            };

            if (slide == "NewProduct")
            {
                var newProduct = new IndexViewModel
                {
                    Products = await _dataContext.Products
                    .OrderByDescending(p => p.CreatedAt).Take(4).Select(p => new ProductViewModel(p.Id, p.Name!, p.Description!, p.Price,
                    p.ProductImages!.Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                    : String.Empty,
                    p.ProductImages!.Skip(1).Take(1).FirstOrDefault() != null
                    ? _fileService.GetFileUrl(p.ProductImages!.Skip(1).Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Products)
                    : String.Empty,
                    p.ProductCategories!.Select(pc => pc.Category)
                    .Select(c => new ProductViewModel.CategoryViewModeL(c!.Title!, c.Parent!.Title!)).ToList(),
                    p.ProductColors!.Select(pc => pc.Color)
                    .Select(c => new ProductViewModel.ColorViewModeL(c!.Name)).ToList(),
                    p.ProductSizes!.Select(ps => ps.Size)
                    .Select(s => new ProductViewModel.SizeViewModeL(s!.Title)).ToList(),
                    p.ProductTags!.Select(ps => ps.Tag)
                    .Select(s => new ProductViewModel.TagViewModel(s!.Title)).ToList()))
                .ToListAsync(),
                };

                return View(newProduct);
            }

            return View(model);
        }
    }
}
