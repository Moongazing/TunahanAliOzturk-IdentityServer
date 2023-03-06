using Microsoft.AspNetCore.Identity;
using TAO.IdentityApp.Web.Models;

namespace TAO.IdentityApp.Web.ValidationRules.CustomValidator
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if(password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new()
                {
                    Code = "PasswordCantContainUserName",
                    Description = "Password can't contains username.",

                });
            }
            if(password!.ToLower().StartsWith("1234567890"))
            {
                errors.Add(new()
                {
                    Code = "PasswordCantStartWithADigit",
                    Description = "Password can't start with a digit."
                });
            }

            if(errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);





        }
    }
}
