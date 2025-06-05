using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Interfaces
{
    /// <summary>
    /// this interface for aplly email
    /// </summary>
    public interface IAppEmailSender
    {
         Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
