using TAO.IdentityApp.Web.Models.Context;
using TAO.IdentityApp.Web.Models;
using TAO.IdentityApp.Web.ValidationRules.CustomValidator;
using FluentValidation.AspNetCore;
using TAO.IdentityApp.Web.ViewModels;
using TAO.IdentityApp.Web.ValidationRules.FluentValidation;

namespace TAO.IdentityApp.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExtension(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_.*";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;

            })
             .AddPasswordValidator<PasswordValidator>()
             .AddUserValidator<UserNameValidator>()
             .AddEntityFrameworkStores<AppDbContext>();

        }
        public static void AddFluentValidationExtensions(this IServiceCollection services)
        {

            services.AddControllersWithViews().AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<SignInViewModelValidator>();
                x.RegisterValidatorsFromAssemblyContaining<SignUpViewModelValidator>();

            });


        }
    }
}
