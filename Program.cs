using Microsoft.EntityFrameworkCore;
using TAO.IdentityApp.Web.Models;
using TAO.IdentityApp.Web.Models.Context;
using TAO.IdentityApp.Web.ValidationRules.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using TAO.IdentityApp.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using TAO.IdentityApp.Web.OptionsModel;
using TAO.IdentityApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationExtensions();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval= TimeSpan.FromMinutes(30);
});

builder.Services.AddIdentityWithExtension();
builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "ToBuyListCookie";
    options.LoginPath = new PathString("/Home/SignIn");
    options.LogoutPath = new PathString("/Members/Logout");
    options.Cookie = cookieBuilder;
    options.ExpireTimeSpan = TimeSpan.FromDays(60);
    options.SlidingExpiration = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
