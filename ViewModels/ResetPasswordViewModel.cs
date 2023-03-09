using System.ComponentModel.DataAnnotations;

namespace TAO.IdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Display(Name ="New Password:")]
        public string Password { get; set; }

        [Display(Name ="New Password Confirm:")]
        public string PasswordConfirm { get; set; }
    }
}
