using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.Interfaces.Saved;

namespace YourTrips.Core.Interfaces.SavedServices
{
    public interface ISavDelJSONModel
    {
        Task<ResultDto> SaveJsonAsync<T>(Guid userId, string json) where T : class, ISavedEntity, new();

    }
}
