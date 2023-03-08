using TAO.IdentityApp.Web.Models.Context;
using TAO.IdentityApp.Web.Models;
using TAO.IdentityApp.Web.ValidationRules.CustomValidator;
using FluentValidation.AspNetCore;
using TAO.IdentityApp.Web.ViewModels;
using TAO.IdentityApp.Web.ValidationRules.FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace TAO.IdentityApp.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExtension(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_.*";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;


                //Lockout

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
             .AddPasswordValidator<PasswordValidator>()
             .AddUserValidator<UserNameValidator>()
             .AddDefaultTokenProviders()
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
