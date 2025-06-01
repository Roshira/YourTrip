using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs;
using YourTrips.Core.DTOs.Route.Saved;

namespace YourTrips.Core.Interfaces.Routes.Saved
{
    public interface ISavDelJSONModel
    {
        Task<ResultDto> SaveJsonAsync<T>(SavedDto savedDto)
     where T : class, ISavedEntity, new();
      Task  <ResultDto> DeleteJsonAsync<T>(DeleteSavedDto deleteSavedDto)
     where T : class, ISavedEntity, new ();

    }
}
