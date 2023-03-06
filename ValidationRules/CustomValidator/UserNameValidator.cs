using Microsoft.AspNetCore.Identity;
using TAO.IdentityApp.Web.Models;

namespace TAO.IdentityApp.Web.ValidationRules.CustomValidator
{
    public class UserNameValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();

            var isDigit = int.TryParse(user.UserName[0]!.ToString(), out _);

            if(isDigit)
            {
                errors.Add(new()
                {
                    Code = "UserNameStartsWithDigit",
                    Description = "Username can't starts with digit."
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
