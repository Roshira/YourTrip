using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    public interface ISaveDelId
    {
        Task<ResultDto> SaveIdAsync<T>(string externalId, int routeId)
         where T : class, ISavedEntity, new();
    }
}
