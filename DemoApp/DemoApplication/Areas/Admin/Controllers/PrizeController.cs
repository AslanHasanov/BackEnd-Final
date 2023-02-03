using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using DemoApplication.Areas.Admin.ViewModels.Prize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Database.Models;
using DemoApplication.Contracts.File;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("admin")]
    [Route("admin/prize")]
    [Authorize(Roles = "admin")]
    public class PrizeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PrizeController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }


        #region List'

        [HttpGet("List", Name = "admin-prize-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Prizes
                .Select(r => new ListItemViewModel(r.Id, _fileService.GetFileUrl(r.ImageNameInFileSystem, Contracts.File.UploadDirectory.Prize), r.CreatedAt))
                .ToListAsync();


            return View(model);
        }

        #endregion

        #region Add'

        [HttpGet("add", Name = "admin-prize-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-prize-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var imageNameInSystem = await _fileService.UploadAsync(model.Image, Contracts.File.UploadDirectory.Prize);

            var prize = new Prize
            {
                ImageName = model.Image.FileName,
                ImageNameInFileSystem = imageNameInSystem,
            };

            await _dataContext.AddAsync(prize);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-prize-list");
        }
        #endregion

        #region Delete'

        [HttpPost("delete/{id}", Name = "admin-prize-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var prize = await _dataContext.Prizes.FirstOrDefaultAsync(r => r.Id == id);

            if (prize is null) return NotFound();

            await _fileService.DeleteAsync(prize.ImageNameInFileSystem, UploadDirectory.Prize);
            _dataContext.Prizes.Remove(prize);
            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-prize-list");
        } 
        #endregion
    }
}
