using Library.Service.Contract;
using Library.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class AccountController : Controller
	{
        private readonly IUserService userService;
  
        public AccountController(IUserService userService)
		{
            this.userService = userService;
		}

        #region Views

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            var model = new UserRegistrationModel();
            return View(model);
        }        

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            var model = new UserLoginModel();
            return View(model);
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }        

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public IActionResult AccountLocked()
        {
            return View();
        }

        #endregion

        #region Actions

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationModel request)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userService.CreateUserAsync(request);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    if (result.Errors.Any())
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("message", error.Description);
                        }
                    }
                    return View(request);
                }
            }
            return View(request);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUserAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Dashboard));
                }
                else if (result.IsLockedOut)
                {
                    return View(nameof(AccountLocked));
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this.userService.SignOutAsync();
            return RedirectToAction(nameof(Login), "account");
        }

        #endregion
    }
}