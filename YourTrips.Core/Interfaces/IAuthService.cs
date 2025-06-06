using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.Auth;

namespace YourTrips.Core.Interfaces
{
    /// <summary>
    /// Interface register
    /// </summary>
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}
