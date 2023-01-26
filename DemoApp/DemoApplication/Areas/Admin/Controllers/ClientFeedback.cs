using DemoApplication.Areas.Admin.ViewModels.ClientFeedback;
using DemoApplication.Database;
using DemoApplication.Migrations;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/clientFeedback")]
    public class ClientFeedback : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ClientFeedback(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List'

        [HttpGet("list", Name = "admin-clientfeedback-list")]

        public async Task<IActionResult> List()
        {
            var model = await _dataContext.ClientFeedbacks
                .Select(c => new ListItemViewModel(c.Id, c.User.FirstName, c.User.Roles!.Name,c.CreatedAt,c.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion


        #region Add'

        [HttpGet("add", Name = "admin-clientfeedback-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Users = await _dataContext.Users.Select(u => new UserViewModel(u.Id, u.FirstName, u.Roles!.Name))
                .ToListAsync(),
            };

            return View(model);
        }


        [HttpPost("add", Name = "admin-clientfeedback-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = new DemoApplication.Database.Models.ClientFeedback
            {
                Content = model.Content,
                UserId = model.UserId,
            };
            await _dataContext.ClientFeedbacks.AddAsync(client);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-clientfeedback-list");
        } 
        #endregion
    }
}
