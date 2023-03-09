using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TAO.IdentityApp.Web.Models;
using TAO.IdentityApp.Web.ViewModels;
using TAO.IdentityApp.Web.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TAO.IdentityApp.Web.Services;

namespace TAO.IdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IEmailService emailService)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SignUp()
        {

            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Wrong email or password!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user: hasUser, password: request.Password, isPersistent: request.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>()
                {
                    "You cannot login for 3 minutes."
                });
                return View();
            }

            ModelState.AddModelErrorList(new List<string>() {

                "Wrong email or password.",
                $" Number of failed logins: { await _userManager.GetAccessFailedCountAsync(hasUser)}"

            });
            return View();




        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email, }, request.Password);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Successed.";
                return RedirectToAction(nameof(HomeController.SignUp));
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());

            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "No registered user found for this email address!");
                return View();
            }


            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink = Url.Action("ResetPassword", "Home", new
            {
                userId = hasUser.Id,
                Token = passwordResetToken
            },
            HttpContext.Request.Scheme
           
            );

            await _emailService.SendResetPasswordMail(passwordResetLink!,hasUser.Email!);

            TempData ["SuccessMessage"]= "Password reset link sent it your e-mail.";
            return RedirectToAction(nameof(ForgetPassword));

            
        }

        public  IActionResult ResetPassword(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View(); 
           
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            var userId = TempData["userId"]!.ToString();
            var token = TempData["token"]!.ToString();

            var hasUser = await _userManager.FindByIdAsync(userId!);
            if(hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(hasUser,token,request.Password);
            if(result.Succeeded)
            {
                TempData["SuccessMessage"] = "Password reseted.";
            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x=>x.Description).ToList());
               
            }
            return View();

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}