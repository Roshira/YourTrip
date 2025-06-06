using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourTrips.Application.DTOs.Admin.Data
{
    public class UserDto
    {
        public Guid Id { get; set; }         
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
