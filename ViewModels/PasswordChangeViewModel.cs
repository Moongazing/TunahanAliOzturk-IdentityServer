using System.ComponentModel.DataAnnotations;

namespace TAO.IdentityApp.Web.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current Password:")]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]

        [Display(Name = "New Password:")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]

        [Display(Name = "New Password Confirm:")]
        public string NewPasswordConfirm { get; set; }
    }
}
