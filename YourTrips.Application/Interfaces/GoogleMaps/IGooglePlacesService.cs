using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.GoogleMaps;
using YourTrips.Core.DTOs.GoogleMaps.PlaceDetail;

namespace YourTrips.Application.Interfaces.GoogleMaps
{
    public interface IGooglePlacesService
    {


        public Task<PlaceDetailsDto> GetFullPlaceDetailsAsync(string placeId);

        public Task<List<PlaceSearchResultDto>> GetPlacesInfoAsync(string locationName);
    }
}
