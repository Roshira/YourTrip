using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.Interfaces.Interfaces
{
    public interface IAmadeusAuthService
    {
        Task<string> GetAccessTokenAsync();
    }
}
