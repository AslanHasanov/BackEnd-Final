using DemoApplication.Areas.Admin.ViewModels.Slider;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Sliders.Select(s => new ListItemViewModel(s.Id, s.HeadTitle, s.MainTitle, s.Content,_fileService
                .GetFileUrl(s.BackgroundImageInFileSystem, UploadDirectory.Slider), s.Button, s.ButtonRedirectUrl, s.CreatedAt,s.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region add
        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var imageNameInSystem = await _fileService.UploadAsync(model.BackgroundImage, UploadDirectory.Slider);

            AddSlider(model.BackgroundImage.FileName, imageNameInSystem);

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-slider-list");

            async void AddSlider(string imageName, string imageNameInSystem)
            {
                var slider = new Slider
                {
                    HeadTitle = model.HeadTitle,
                    MainTitle = model.MainTitle,
                    Content = model.Content,
                    BackgroundImage = imageName,
                    BackgroundImageInFileSystem = imageNameInSystem,
                    Button = model.Button,
                    ButtonRedirectUrl = model.ButtonRedirectUrl,
                   
                };

                await _dataContext.Sliders.AddAsync(slider);
            }

        }

        #endregion


        #region update
        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = slider.Id,
                HeadTitle = slider.HeadTitle,
                MainTitle = slider.MainTitle,
                Content = slider.Content,
                BackgroundImageUrl = _fileService.GetFileUrl(slider.BackgroundImageInFileSystem, UploadDirectory.Slider),
                Button = slider.Button,
                ButtonRedirectUrl = slider.ButtonRedirectUrl
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {

            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (slider is null)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _fileService.DeleteAsync(model.BackgroundImage.FileName, UploadDirectory.Slider);

            var backGroundImageFileSystem = await _fileService.UploadAsync(model.BackgroundImage, UploadDirectory.Slider);


            await UpdateSliderImage(model.BackgroundImage.FileName, backGroundImageFileSystem);

            return RedirectToRoute("admin-slider-list");


            async Task UpdateSliderImage(string imageName, string imageNameInSystem)
            {
                slider.HeadTitle = model.HeadTitle;
                slider.MainTitle = model.MainTitle;
                slider.Content = model.Content;
                slider.BackgroundImage = imageName;
                slider.BackgroundImageInFileSystem = imageNameInSystem;
                slider.Button = model.Button;
                slider.ButtonRedirectUrl = model.ButtonRedirectUrl;
                slider.UpdatedAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(slider.BackgroundImageInFileSystem, UploadDirectory.Slider);
            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        } 
        #endregion
    }
}
