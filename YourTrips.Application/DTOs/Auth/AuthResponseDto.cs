using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.Auth
{
    /// <summary>
    /// Auth response dto
    /// </summary>
    public class AuthResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
