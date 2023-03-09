using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using TAO.IdentityApp.Web.Extensions;
using TAO.IdentityApp.Web.Models;
using TAO.IdentityApp.Web.ViewModels;

namespace TAO.IdentityApp.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public MembersController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name);
            var userViewModel = new UserViewModel
            {
                Email = currentUser!.Email,
                UserName = currentUser.UserName,
                PhoneNumber = currentUser.PhoneNumber
            };

            return View(userViewModel);

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();


        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name);

            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.CurrentPassword);
            if (!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Old password wrong.");
                return View();
            }
            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, request.CurrentPassword, request.NewPassword);

            if (!resultChangePassword.Succeeded)
            {
                ModelState.AddModelErrorList(resultChangePassword.Errors.Select(x => x.Description).ToList());
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.NewPassword, true, false);

            TempData["SuccessMessage"] = "Your password has been successfully changed.";
            return View();
        }
    }
}
