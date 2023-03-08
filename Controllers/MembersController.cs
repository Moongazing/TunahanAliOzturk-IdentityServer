using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using TAO.IdentityApp.Web.Models;

namespace TAO.IdentityApp.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        public MembersController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();

       
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

           
        }
    }
}
