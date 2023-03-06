using FluentValidation;
using System.Text.RegularExpressions;
using TAO.IdentityApp.Web.ViewModels;

namespace TAO.IdentityApp.Web.ValidationRules.FluentValidation
{
    public class SignUpViewModelValidator:AbstractValidator<SignUpViewModel>
    {
        public SignUpViewModelValidator()

        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage($"Please enter a valid user name.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Please enter a valid phone number \n Example: 555555555");
          //  RuleFor(x => x.Password).NotEmpty().Matches(x=>x.PasswordConfirm).WithMessage("Password must be match.");
          //  RuleFor(x => x.PasswordConfirm).NotEmpty().Matches(x=>x.Password).WithMessage("Password must be match.");
        }
       
       /* private bool IsPasswordValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(arg);
        }*/
    }
}
