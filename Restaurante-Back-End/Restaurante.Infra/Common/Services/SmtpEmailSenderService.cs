using Restaurante.Application.Common.Models;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Infra.Common.Settings;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Common.Services
{
    internal class SmtpEmailSenderService : IMessageSenderService<EmailMessage>
    {
        private readonly SmtpEmailSettings _smtpEmailSettings;
        public SmtpEmailSenderService(SmtpEmailSettings smtpEmailSettings) =>
            _smtpEmailSettings = smtpEmailSettings;

        public async Task<SenderResponse> SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            try
            {
                using var emailMsg = new MailMessage
                {
                    From = new MailAddress(_smtpEmailSettings.Email)
                };
                emailMsg.To.Add(message.To);
                emailMsg.Subject = message.Subject;
                emailMsg.Body = message.Body;

                using var smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_smtpEmailSettings.Email, _smtpEmailSettings.Password);

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;

                smtpClient.Timeout = 20_000;
                await smtpClient.SendMailAsync(emailMsg, cancellationToken);
                return new SenderResponse(true, string.Empty);
            }
            catch (Exception e)
            {
                return new SenderResponse(false, e.Message);
            }
        }
    }
}
