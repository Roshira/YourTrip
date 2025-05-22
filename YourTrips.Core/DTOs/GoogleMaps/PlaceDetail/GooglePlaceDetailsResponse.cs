using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YourTrips.Core.DTOs.GoogleMaps.PlaceDetail.ResponseClase;

namespace YourTrips.Core.DTOs.GoogleMaps.PlaceDetail
{
    public class GooglePlaceDetailsResponse
    {
        public GooglePlaceResult Result { get; set; }
    }

}
