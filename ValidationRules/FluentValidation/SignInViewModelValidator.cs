using FluentValidation;
using TAO.IdentityApp.Web.ViewModels;

namespace TAO.IdentityApp.Web.ValidationRules.FluentValidation
{
    public class SignInViewModelValidator:AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
