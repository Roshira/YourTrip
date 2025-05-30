﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;

namespace YourTrips.Core.Interfaces
{
    public interface IRewriteUserName
    {
        public Task<ResultDto> RewriteUserNameAsync(ClaimsPrincipal userClaims, string newUserName);
    }
}
