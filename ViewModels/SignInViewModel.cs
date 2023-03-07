using System.ComponentModel.DataAnnotations;

namespace TAO.IdentityApp.Web.ViewModels
{
    public class SignInViewModel
    {
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Password:")]
        public string Password { get; set; }
        [Display(Name= "Remember Me") ]
        public bool RememberMe { get; set; }
    }
}
