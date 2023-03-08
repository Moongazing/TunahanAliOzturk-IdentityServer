namespace TAO.IdentityApp.Web.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordMail(string resetLink, string ToMail);

    }
}
