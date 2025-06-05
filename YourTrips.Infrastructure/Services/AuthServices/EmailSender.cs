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
    /// <summary>
    /// Service for sending emails using SMTP, implemented with MailKit.
    /// </summary>
    public class EmailSender : IAppEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class with SMTP configuration settings.
        /// </summary>
        /// <param name="smtpSettings">SMTP settings provided via dependency injection.</param>
        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        /// <summary>
        /// Sends an email asynchronously using the configured SMTP server.
        /// </summary>
        /// <param name="email">The recipient's email address.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="htmlMessage">The HTML content of the email.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="MailKit.Security.AuthenticationException">Thrown if authentication with the SMTP server fails.</exception>
        /// <exception cref="System.Net.Sockets.SocketException">Thrown if the connection to the SMTP server fails.</exception>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
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
