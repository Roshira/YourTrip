using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Common
{
    /// <summary>
    /// Represents the SMTP configuration settings used for sending emails.
    /// </summary>
    public class SmtpSettings
    {
        /// <summary>
        /// Gets or sets the SMTP server address.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the port used to connect to the SMTP server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the username used to authenticate with the SMTP server.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password used to authenticate with the SMTP server.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the display name of the sender.
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the sender's email address.
        /// </summary>
        public string FromEmail { get; set; }
    }
}
