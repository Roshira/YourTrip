using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.Common;
using YourTrips.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace YourTrips.Infrastructure.Services.AuthServices
{
    public class EmailSender : IAppEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync (string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, false);
            await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

        }
    }
}
