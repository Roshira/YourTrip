using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    public interface ISavDelJSONModel
    {
        Task<ResultDto> SaveJsonAsync<T>(string json, int routeId)
     where T : class, ISavedEntity, new();


    }
}
