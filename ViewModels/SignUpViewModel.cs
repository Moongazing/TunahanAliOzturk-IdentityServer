using System.ComponentModel.DataAnnotations;

namespace TAO.IdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {
        [Display(Name = "User Name:")]
        public string UserName { get; set; }
        [Display(Name = "Email:")]

        public string Email { get; set; }
        [Display(Name = "Phone:")]

        public string Phone { get; set; }
        [Display(Name = "Password")]

        public string Password { get; set; }

        [Display(Name = "Password Confirm:")]

        public string PasswordConfirm { get; set; }
    }
}
