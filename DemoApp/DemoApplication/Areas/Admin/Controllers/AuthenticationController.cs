using DemoApplication.Areas.Admin.ViewModels.Authentication;
using DemoApplication.Contracts.Identity;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/auth")]
    public class AuthenticationController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        #region Login'

        [HttpGet("login", Name = "admin-auth-login")]
        public async Task<IActionResult> Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "admin-auth-login")]
        public async Task<IActionResult> Login(LoginViewModel? model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password not correct");
                return View(model);
            }

            if (!await _dataContext.Users.AnyAsync(u => u.Roles!.Name == RoleNames.ADMIN))
            {
                ModelState.AddModelError(String.Empty, "Role is not Admin");
                return View(model);
            }

            if (!await _dataContext.Users.AnyAsync(u => u.Email == model.Email && u.Roles!.Name == RoleNames.ADMIN))
            {
                ModelState.AddModelError(String.Empty, "Role is not Admin");
                return View(model);
            }

            return RedirectToRoute("admin-slider-list");

        } 
        #endregion

        #region LogOut'

        [HttpGet("logout", Name = "admin-auth-logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("admin-auth-login");
        } 
        #endregion
    }
}
