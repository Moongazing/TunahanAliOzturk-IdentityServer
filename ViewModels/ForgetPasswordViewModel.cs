using System.ComponentModel.DataAnnotations;

namespace TAO.IdentityApp.Web.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}
