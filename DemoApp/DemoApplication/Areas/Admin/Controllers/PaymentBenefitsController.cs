using DemoApplication.Areas.Admin.ViewModels.PaymentBenefits;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/paymentBenefits")]
    public class PaymentBenefitsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentBenefitsController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-paymentbenefits-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.PaymentBenefits.Select(pb => new ListItemViewModel(pb.Id, pb.Title, pb.Content, _fileService
                .GetFileUrl(pb.BackgroundImageInFileSystem, UploadDirectory.PaymentBenefits), pb.CreatedAt, pb.UpdatedAt)).ToListAsync();

            return View(model);
        } 
        #endregion


        #region add

        [HttpGet("add", Name = "admin-paymentbenefits-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-paymentbenefits-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {

            if (ModelState.IsValid) return View(model);

            var imageNameInSystem = await _fileService.UploadAsync(model.BackgroundImage, UploadDirectory.PaymentBenefits);

            AddPaymnetBenefit(model.BackgroundImage.FileName, imageNameInSystem);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-paymentbenefits-list");


            async void AddPaymnetBenefit(string imageName, string imageNameInSystem)
            {
                var paymentBenefit = new PaymentBenefit
                {
                    Title = model.Title,
                    Content = model.Content,
                    BackgroundImage = imageName,
                    BackgroundImageInFileSystem = imageNameInSystem,
                  
                };

                await _dataContext.PaymentBenefits.AddAsync(paymentBenefit);
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-paymentbenefits-update")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var paymentBenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(pb => pb.Id == id);
            if (paymentBenefit is null)
            {
                return NotFound();
            }
            var model = new UpdateViewModel
            {
                Id = paymentBenefit.Id,
                Title = paymentBenefit.Title,
                Content = paymentBenefit.Content,
                BackgroundImageInFileSystem = _fileService.GetFileUrl(paymentBenefit.BackgroundImageInFileSystem, UploadDirectory.PaymentBenefits),
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-paymentbenefits-update")]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {

            var paymentBenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(pb => pb.Id == model.Id);
            if (paymentBenefit is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _fileService.DeleteAsync(paymentBenefit.BackgroundImageInFileSystem, UploadDirectory.PaymentBenefits);

            var backGroundImageFileSystem = await _fileService.UploadAsync(model.BackgroundImage, UploadDirectory.PaymentBenefits);


            await UpdatePaymentBenefit(model.BackgroundImage.FileName, backGroundImageFileSystem);

            return RedirectToRoute("admin-paymentbenefits-list");


            async Task UpdatePaymentBenefit(string imageName, string imageNameInSystem)
            {
                paymentBenefit.Title = model.Title;
                paymentBenefit.Content = model.Content;
                paymentBenefit.BackgroundImage = imageName;
                paymentBenefit.BackgroundImageInFileSystem = imageNameInSystem;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion


        #region Delete

        [HttpPost("delete/{id}", Name = "admin-paymentbenefits-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var paymentbenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(pb => pb.Id == id);
            if (paymentbenefit is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(paymentbenefit.BackgroundImageInFileSystem, UploadDirectory.PaymentBenefits);
            _dataContext.PaymentBenefits.Remove(paymentbenefit);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-paymentbenefits-list");
        }
        #endregion
    }
}
