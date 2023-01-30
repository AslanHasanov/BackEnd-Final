using DemoApplication.Areas.Admin.ViewModels.Product;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    [Authorize(Roles = "admin")]

    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(DataContext dataContext, IFileService fileService, ILogger<ProductController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        #region List'

        [HttpGet("list", Name = "admin-product-list")]

        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Products.Select(p => new ListItemViewModel(p.Id, p.Name!, p.Rate, p.Description!, p.Price,
                p.CreatedAt, p.UpdatedAt,
                p.ProductCategories!.Select(pc => pc.Category).
                Select(c => new ListItemViewModel.CategoryViewModeL(c.Title!, c.Parent!.Title!)).ToList(),
                p.ProductColors!.Select(pc => pc.Color).Select(c => new ListItemViewModel.ColorViewModeL(c!.Name)).ToList(),
                p.ProductSizes!.Select(ps => ps.Size).Select(s => new ListItemViewModel.SizeViewModeL(s!.Title)).ToList(),
                p.ProductTags!.Select(ps => ps.Tag).Select(s => new ListItemViewModel.TagViewModel(s!.Title)).ToList()))
                .ToListAsync();


            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.Categories.Select(c => new CategoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                Sizes = await _dataContext.Sizes.Select(s => new SizeListItemViewModel(s.Id, s.Title)).ToListAsync(),
                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                Tags = await _dataContext.Tags.Select(t => new TagListItemViewModel(t.Id, t.Title)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) 
            { 
                return GetView(model); 
            }
          

            foreach (var categoryId in model.CategoryIds!)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with ({categoryId}) not found in database ");

                    return GetView(model);
                }

            }

            foreach (var sizeId in model.SizeIds!)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with ({sizeId}) not found in database ");

                    return GetView(model);
                }

            }

            foreach (var colorId in model.ColorIds!)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Color with ({colorId}) not found in database ");

                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds!)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with ({tagId}) not found in database ");

                    return GetView(model);
                }

            }



            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");



            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.Categories
                   .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                 .ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.Title))
                 .ToList();


                return View(model);
            }


            async void AddProduct()
            {
                var product = new Product
                {
                    Name = model.Name!,
                    Rate = model.Rate,
                    Description = model.Description!,
                    Price = model.Price,
                };

                await _dataContext.Products.AddAsync(product);

                foreach (var catagoryId in model.CategoryIds)
                {
                    var productCatagory = new ProductCategory
                    {
                        CategoryId = catagoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCategories.AddAsync(productCatagory);
                }

                foreach (var colorId in model.ColorIds)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }

                foreach (var sizeId in model.SizeIds)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                foreach (var tagId in model.TagIds)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }


            }
        }

        #endregion


        #region Update'

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(c => c.ProductCategories)
                .Include(c => c.ProductColors)
                .Include(s => s.ProductSizes)
                .Include(t => t.ProductTags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = product.Id,
                Name = product.Name!,
                Description = product.Description!,
                Price = product.Price,
                Rate = product.Rate,

                Categories = await _dataContext.Categories
                .Select(c => new CategoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                CategoryIds = product.ProductCategories!
                .Select(pc => pc.CategoryId).ToList(),

                Sizes = await _dataContext.Sizes
                .Select(c => new SizeListItemViewModel(c.Id, c.Title)).ToListAsync(),
                SizeIds = product.ProductSizes!
                .Select(pc => pc.SizeId).ToList(),

                Colors = await _dataContext.Colors
                .Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                ColorIds = product.ProductColors!
                .Select(pc => pc.ColorId).ToList(),

                Tags = await _dataContext.Tags
                .Select(c => new TagListItemViewModel(c.Id, c.Title)).ToListAsync(),
                TagIds = product.ProductTags!
                .Select(pc => pc.TagId).ToList(),

            };

            return View(model);

        }


        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var product = await _dataContext.Products
                    .Include(c => c.ProductCategories)
                    .Include(c => c.ProductColors)
                    .Include(s => s.ProductSizes)
                    .Include(t => t.ProductTags)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null) { return NotFound(); }
           

            if (!ModelState.IsValid) { return GetView(model); }
            

            foreach (var categoryId in model.CategoryIds!)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with ({categoryId}) not found in database ");
                    return GetView(model);
                }

            }

            foreach (var sizeId in model.SizeIds!)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with ({sizeId}) not found in database ");
                    return GetView(model);
                }

            }

            foreach (var colorId in model.ColorIds!)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Color with ({colorId}) not found in database ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds!)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with ({tagId}) not found in database ");
                    return GetView(model);
                }

            }


            UpdateProductAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");


            IActionResult GetView(UpdateViewModel model)
            {

                model.Categories = _dataContext.Categories
                   .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.CategoryIds = product.ProductCategories!.Select(c => c.CategoryId).ToList();

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.SizeIds = product.ProductSizes!.Select(c => c.SizeId).ToList();

                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                 .ToList();

                model.ColorIds = product.ProductColors!.Select(c => c.ColorId).ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.Title))
                 .ToList();

                model.TagIds = product.ProductTags!.Select(c => c.TagId).ToList();

                return View(model);
            }
            async Task UpdateProductAsync()
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Rate = model.Rate;
   

        
                var categoriesInDatacontext = product.ProductCategories!.Select(bc => bc.CategoryId).ToList();
                var categoriesToRemove = categoriesInDatacontext.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDatacontext).ToList();

                product.ProductCategories!.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var productCatagory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCategories.AddAsync(productCatagory);
                }
         

                var colorInDatacontext = product.ProductColors!.Select(bc => bc.ColorId).ToList();
                var colorToRemove = colorInDatacontext.Except(model.ColorIds).ToList();
                var colorToAdd = model.ColorIds.Except(colorInDatacontext).ToList();

                product.ProductColors!.RemoveAll(bc => colorToRemove.Contains(bc.ColorId));


                foreach (var colorId in colorToAdd)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }
          


             
                var sizeInDatacontext = product.ProductSizes!.Select(bc => bc.SizeId).ToList();
                var sizeToRemove = sizeInDatacontext.Except(model.SizeIds).ToList();
                var sizeToAdd = model.SizeIds.Except(sizeInDatacontext).ToList();

                product.ProductSizes!.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


                foreach (var sizeId in sizeToAdd)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

          

                var tagInDatacontext = product.ProductTags!.Select(bc => bc.TagId).ToList();
                var tagToRemove = tagInDatacontext.Except(model.TagIds).ToList();
                var tagToAdd = model.TagIds.Except(tagInDatacontext).ToList();

                product.ProductTags!.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


                foreach (var tagId in tagToAdd)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }
            
            }
            
        }
        #endregion


        #region Delete
        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (products is null)  return NotFound(); 

           

            _dataContext.Products.Remove(products);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");
        }


        #endregion

    }
}
