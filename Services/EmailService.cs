using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TAO.IdentityApp.Web.OptionsModel;

namespace TAO.IdentityApp.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendResetPasswordMail(string resetLink, string ToMail)
        {
            var smtpClient = new SmtpClient();

            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod= SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials= false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email,_emailSettings.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToMail);
            mailMessage.Subject = "Localhost | Reset Password Link";
            mailMessage.Body = @$"
            <h4>For reset password clink on the link.</h4>
            <p>
                <a href='{resetLink}'>Password Reset Link</a>
             <p>";
            mailMessage.IsBodyHtml= true;
            await smtpClient.SendMailAsync(mailMessage);

        }
    }
}
