using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TAO.IdentityApp.Web.Models;

namespace TAO.IdentityApp.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        public MembersController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

            
        }
    }
}
