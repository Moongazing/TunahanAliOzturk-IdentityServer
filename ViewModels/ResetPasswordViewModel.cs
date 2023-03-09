using System.ComponentModel.DataAnnotations;

namespace TAO.IdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name ="New Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]

        [Display(Name ="New Password Confirm:")]
        public string PasswordConfirm { get; set; }
    }
}
