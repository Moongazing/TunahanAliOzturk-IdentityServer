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
                    Code = "PasswordContainsUserName",
                    Description = "Password can't contains username.",

                });
            }
            if(password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new()
                {
                    Code = "PasswordTakeConsecutiveNumbers",
                    Description = "Password can't take consecutive numbers."
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
