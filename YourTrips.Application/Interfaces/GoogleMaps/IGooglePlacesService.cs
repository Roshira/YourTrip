using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTrips.Application.DTOs.GoogleMaps;
using YourTrips.Application.DTOs.GoogleMaps.PlaceDetail;

namespace YourTrips.Application.Interfaces.GoogleMaps
{
    public interface IGooglePlacesService
    {


        public Task<PlaceDetailsDto> GetFullPlaceDetailsAsync(string placeId);

        public Task<List<PlaceSearchResultDto>> GetPlacesInfoAsync(string locationName);
    }
}
