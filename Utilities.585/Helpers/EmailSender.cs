using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;
using Utilities._585.Models;

namespace Utilities._585.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpOptions options;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(IOptions<SmtpOptions> options, ILogger<EmailSender> logger)
        {
            this.options = options.Value;
            this.logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                using var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(options.SenderEmail, options.SenderName, Encoding.UTF8);
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Subject = subject;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = htmlMessage;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.IsBodyHtml = true;

                using var smtpClient = new SmtpClient(options.Host, options.Port);
                smtpClient.EnableSsl = options.EnableSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;

                if (!string.IsNullOrEmpty(options.UserName.Trim()) && !string.IsNullOrEmpty(options.Password.Trim()))
                {
                    smtpClient.Credentials = new NetworkCredential(options.UserName, options.Password);
                }

                await smtpClient.SendMailAsync(mailMessage);

                logger.LogInformation("Eposta gönderimi başarılı, alıcı: {Email}", email);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Eposta gönderimi sırasında hata oluştu, alıcı: {Email}", email);
                throw;
            }
        }
    }
}
